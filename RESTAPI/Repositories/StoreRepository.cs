using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Exceptions;
using RESTAPI.Models;
using RESTAPI.Utils;

namespace RESTAPI.Repositories
{
    public interface IStoreRepository
    {
        IEnumerable<Store> GetAll();
        IEnumerable<Store> GetByCategory(int categoryId);
        IEnumerable<Store> GetPopular(int limit);
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

        public IEnumerable<Store> GetAll()
        {
            var list = _context.Store.Include(s => s.Category).ToList();
            SetImgPathHostName(list);

            return list;
        }

        public IEnumerable<Store> GetByCategory(int categoryId)
        {
            var list = _context.Store.Where(s => s.Category.CategoryId == categoryId);
            SetImgPathHostName(list);

            return list;
        }

        public IEnumerable<Store> GetPopular(int limit)
        {
            if (limit < 0) throw new StoreException("Limit must not be less than 0");

            var list = _context.Establishment.OrderByDescending(e => e.Visited).Select(e => e.Store).Distinct()
                .Take(limit);
            SetImgPathHostName(list);

            return list;
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
                $"/img/{newFileName}";
            var relPath = $"wwwroot/img/{newFileName}";
            using (var stream = new FileStream(relPath, FileMode.Create))
            {
                imageFormFile.CopyTo(stream);
            }

            store.ImgPath = imgPath;
            _context.Store.Update(store);
            _context.SaveChanges();

            SetImgPathHostName(store);

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

        #region Helpers

        private void SetImgPathHostName(IEnumerable<Store> list)
        {
            foreach (var store in list)
            {
                store.ImgPath = $"{AppSettings.HostName}{store.ImgPath}";
            }
        }

        private void SetImgPathHostName(Store store)
        {
            store.ImgPath = $"{AppSettings.HostName}{store.ImgPath}";
        }

        #endregion
    }
}