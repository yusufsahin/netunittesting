using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineStore.API.Models;


namespace OnlineStore.API.Test
{
    [TestClass]
    public class EFStoreRepositoryTest
    {

        private StoreDbContext _context;
        private EFStoreRepository _repository;
        private IConfiguration _configuration;


        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();

            var options = new DbContextOptionsBuilder<StoreDbContext>()
                          .UseSqlServer(_configuration.GetConnectionString("OnlineStoreConnection"))
                          .Options;

            _context = new StoreDbContext(options);

            // Optionally, ensure the database is clean before running tests
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Seed the database with test data
            _context.Products.Add(new Product { ProductID = 1, Name = "Test Product 1", Description = "Description 1", Price = 10.00m, Category = "Category 1" });
            _context.Products.Add(new Product { ProductID = 2, Name = "Test Product 2", Description = "Description 2", Price = 20.00m, Category = "Category 2" });
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products OFF");

            _repository = new EFStoreRepository(_context);

        }

        [TestMethod]
        public void CanGetAllProducts()
        {
            var products = _repository.Products.ToList();
            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]

        public void CanGetProductById() { 
            var product = _repository.Products.FirstOrDefault(p=>p.ProductID == 1);

            Assert.IsNotNull(product);
            Assert.AreEqual(1, product.ProductID);
            Assert.AreEqual("Test Product 1",product.Name);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
