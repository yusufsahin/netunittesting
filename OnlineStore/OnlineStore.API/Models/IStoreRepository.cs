namespace OnlineStore.API.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }

   
}
