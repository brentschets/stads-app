using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using RESTAPI.Data;
using RESTAPI.Exceptions;
using RESTAPI.Models;

namespace RESTAPI.Repositories
{
    public interface IStoreRepository
    {
        Store Create(Store store, int categoryId, IFormFile imageFormFile);
        void Delete(int storeId);
    }

    public class StoreRepository : IStoreRepository
    {
        private readonly RESTAPIContext _context;

        public StoreRepository(RESTAPIContext context)
        {
            _context = context;
        }

        public Store Create(Store store, int categoryId, IFormFile imageFormFile)
        {
            var category = _context.Category.Find(categoryId);
            store.Category = category ?? throw new StoreException($"The category with id {categoryId} does not exist");

            if (_context.Store.Any(s => s.Name == store.Name))
                throw new StoreException($"The store with name {store.Name} already exists");

            _context.Store.Add(store);
            _context.SaveChanges();

            var newFileName = $"{store.StoreId}{Path.GetExtension(imageFormFile.FileName)}";
            var imgPath =
                $"stadsapprestapi.azurewebsites.net/img/{newFileName}";
            var relPath = $"wwwroot/img/{newFileName}";
            using (var stream = new FileStream(relPath, FileMode.Create))
            {
                imageFormFile.CopyTo(stream);
            }

            store.ImgPath = imgPath;
            _context.Store.Update(store);
            _context.SaveChanges();

            return store;
        }

        public void Delete(int storeId)
        {
            var store = _context.Store.Find(storeId);
            if (store != null)
            {
                _context.Store.Remove(store);
                _context.SaveChanges();
            }
        }
    }
}