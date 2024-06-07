using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStore.API.Models;
using System.Linq;

namespace OnlineStore.API.Test
{
    [TestClass]
    public class EFStoreRepositoryTest
    {
        private StoreDbContext _context;
        private EFStoreRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "OnlineStoreTest")
                .Options;

            _context = new StoreDbContext(options);

            // Seed the in-memory database with test data
            _context.Products.Add(new Product { Name = "Test Product 1", Description = "Description 1", Price = 10.00m, Category = "Category 1" });
            _context.Products.Add(new Product { Name = "Test Product 2", Description = "Description 2", Price = 20.00m, Category = "Category 2" });
            _context.SaveChanges();

            _repository = new EFStoreRepository(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void CanGetAllProducts()
        { 

            var products = _repository.Products.ToList();
            Assert.AreEqual(2, products.Count);

        }

        [TestMethod]
        public void CanGetProductById()
        {
            
            var addedProduct = _context.Products.First(p => p.Name == "Test Product 1");
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == addedProduct.ProductID);
            Assert.IsNotNull(product);
            Assert.AreEqual(addedProduct.ProductID, product.ProductID);
            Assert.AreEqual("Test Product 1", product.Name);

        }

        [TestMethod]
        public void CanAddProduct()
        {
           

            var newProduct = new Product()
            {
                Name="Test Product 3",
                Description="Description",
                Price=30.00m,
                Category="Category 3"
            };

            _repository.AddProduct(newProduct);
            _repository.SaveChanges();

            var products= _repository.Products.ToList();
            Assert.AreEqual(3, products.Count);
            Assert.IsTrue(products.Any(p => p.Name == "Test Product 3"));
        }

        [TestMethod]
        public void CanUpdateProduct()
        {
            // Arrange
            var product = _context.Products.First(p => p.Name == "Test Product 1");
            product.Name = "Updated Product";

            //Act
            _repository.UpdateProduct(product);
            _repository.SaveChanges();

            //Assert
            var updatedProduct = _repository.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            Assert.AreEqual("Updated Product", updatedProduct.Name);

        }

        [TestMethod]
        public void CanDeleteProduct()
        {
           
            // Arrange
            var product = _context.Products.First(p => p.Name == "Test Product 1");

            //Act
            _repository.DeleteProduct(product);
            _repository.SaveChanges();

            var products = _repository.Products.ToList();

            //Assert
            Assert.AreEqual (1, products.Count);
            Assert.IsFalse(products.Any(p => p.Name == "Test Product 1"));
        }
    }
}
