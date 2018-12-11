using RESTAPI.Data;
using RESTAPI.Exceptions;
using RESTAPI.Models;

namespace RESTAPI.Repositories
{
    public interface IStoreRepository
    {
        void CreateStore(string name, string description, int categoryId);
    }

    public class StoreRepository : IStoreRepository
    {
        private readonly RESTAPIContext _context;

        public StoreRepository(RESTAPIContext context)
        {
            _context = context;
        }

        public void CreateStore(string name, string description, int categoryId)
        {
            var category = _context.Category.Find(categoryId);
            if (category == null) throw new AuthenticationException($"Category with id {categoryId} does not exist");

            var store = new Store
            {
                Name = name,
                Description = description,
                Category = category
            };

            _context.Store.Add(store);
            _context.SaveChanges();
        }
    }
}