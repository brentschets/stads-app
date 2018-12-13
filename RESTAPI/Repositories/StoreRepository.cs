using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        Store Create(Store store, int categoryId, string image, string fileName, int userId);
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

        public Store Create(Store store, int categoryId, string image, string fileName, int userId)
        {
            var category = _context.Category.Find(categoryId);
            store.Category = category ?? throw new StoreException($"The category with id {categoryId} does not exist");

            if (_context.Store.Any(s => s.Name == store.Name))
                throw new StoreException($"The store with name {store.Name} already exists");

            _context.Store.Add(store);
            _context.SaveChanges();

            var newFileName = $"{store.StoreId}{Path.GetExtension(fileName)}";
            var imgPath =
                $"/img/{newFileName}";
            var relPath = $"wwwroot/img/{newFileName}";
            File.WriteAllBytes(relPath, Convert.FromBase64String(image));

            store.ImgPath = imgPath;
            _context.Store.Update(store);

            var user = _context.User.Find(userId);
            if (user == null) throw new StoreException($"User with id {userId} does not exist");
            user.StoreId = store.StoreId;
            _context.Update(user);

            _context.SaveChanges();

            // stop tracking store to avoid persisting absolute path
            var entry = _context.ChangeTracker.Entries().Single(e => e.Entity == store);
            entry.State = EntityState.Detached;

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