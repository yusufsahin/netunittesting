namespace Storium.Web.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }

    }
}
