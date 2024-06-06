using System.Linq;

namespace OnlineStore.API.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void SaveChanges();
    }
}
