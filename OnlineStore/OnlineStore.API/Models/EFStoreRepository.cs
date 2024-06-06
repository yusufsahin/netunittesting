
namespace OnlineStore.API.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;

        public IQueryable<Product> Products => context.Products;
    }
}
