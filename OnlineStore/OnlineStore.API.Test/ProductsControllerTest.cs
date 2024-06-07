using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.API.Controllers;
using OnlineStore.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.API.Test
{
    [TestClass]
    public class ProductsControllerTest
    {
        private Mock<IStoreRepository> _mockRepository;

        private ProductsController _controller;
        private List<Product> _products;

      

        [TestInitialize]
        public void TestInitialize()
        {
            _products = new List<Product>
            {
                new Product { ProductID = 1, Name = "Product 1", Description = "Description 1", Price = 10.0m, Category = "Category 1" },
                new Product { ProductID = 2, Name = "Product 2", Description = "Description 2", Price = 20.0m, Category = "Category 2" }
            };

            _mockRepository = new Mock<IStoreRepository>();
            _mockRepository.Setup(repo => repo.Products).Returns(_products.AsQueryable());
            _mockRepository.Setup(repo => repo.AddProduct(It.IsAny<Product>())).Callback<Product>(p => _products.Add(p));
            _mockRepository.Setup(repo => repo.UpdateProduct(It.IsAny<Product>())).Callback<Product>(p =>
            {
                var index = _products.FindIndex(prod => prod.ProductID == p.ProductID);
                if (index != -1)
                {
                    _products[index] = p;
                }
            });
            _mockRepository.Setup(repo => repo.DeleteProduct(It.IsAny<Product>())).Callback<Product>(p => _products.Remove(p));
            _mockRepository.Setup(repo => repo.SaveChanges());

            _controller = new ProductsController(_mockRepository.Object);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsAllProducts()
        {
  
            var result = _controller.GetAllProducts() as OkObjectResult;
            var products= result.Value as List<Product>;

            Assert.IsNotNull(products);
            Assert.AreEqual(2, products.Count);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void GetProductById_ExistingId_ReturnsProduct()
        {
            var result = _controller.GetProductById(1) as OkObjectResult;
            var product = result.Value as Product;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(1, product.ProductID);

        }

        [TestMethod]
        public void GetProductById_NonExistingId_ReturnsNotFound()
        {
            var result = _controller.GetProductById(99);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            /*
             *  var result = _controller.GetProductById(99) as NotFoundResult;

                //Assert.IsInstanceOfType(result, typeof(NotFoundResult));
                Assert.AreEqual(404, result.StatusCode);
             * */
        }

        [TestMethod]
        public void CreateProduct_ValidProduct_ReturnsCreatedAtAction()
        {
 
            var newProduct = new Product { Name = "Product 3", Description = "Description 3", Price = 30.0m, Category = "Category 3" };

            var result = _controller.CreateProduct(newProduct) as CreatedAtActionResult;
            var createdProduct = result.Value as Product;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(newProduct.Name, createdProduct.Name);
        }

        [TestMethod]
        public void UpdateProduct_ExistingId_ReturnsNoContent()
        {
  

            var updatedProduct = new Product { ProductID = 1, Name = "Updated Product", Description = "Updated Description", Price = 15.0m , Category = "Updated Category" };
            var result = _controller.UpdateProduct(1, updatedProduct);
            Assert.IsInstanceOfType(result,typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteProduct_ExistingId_ReturnsNoContent()
        {

            var result= _controller.DeleteProduct(1);
            Assert.IsInstanceOfType(result,typeof (NoContentResult));
            
        }
    }
}
