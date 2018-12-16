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
    public interface IEstablishmentRepository
    {
        Establishment Create(Establishment establishment, int storeId, string image, string fileName);
        IEnumerable<Establishment> GetPopular(int limit);
        IEnumerable<Establishment> GetForStore(int storeId);
        IEnumerable<Establishment> GetForUser(int userId);
        void Delete(int id);
    }

    public class EstablishmentRepository : IEstablishmentRepository
    {
        private readonly RESTAPIContext _context;

        public EstablishmentRepository(RESTAPIContext context)
        {
            _context = context;
        }

        public Establishment Create(Establishment establishment, int storeId, string image, string fileName)
        {
            var store = _context.Store.Find(storeId);
            establishment.Store =
                store ?? throw new EstablishmentException($"The store with id {storeId} does not exist");

            _context.Establishment.Add(establishment);
            _context.SaveChanges();

            var newFileName = $"{establishment.EstablishmentId}{Path.GetExtension(fileName)}";
            var imgPath = $"/img/establishments/{newFileName}";
            var relPath = $"wwwroot/img/establishments/{newFileName}";
            File.WriteAllBytes(relPath, Convert.FromBase64String(image));

            establishment.ImgPath = imgPath;
            _context.Establishment.Update(establishment);
            _context.SaveChanges();

            // stop tracking establishment to avoid persisting absolute path
            var entry = _context.ChangeTracker.Entries().Single(e => e.Entity == establishment);
            entry.State = EntityState.Detached;

            SetImgPathHostName(establishment);

            return establishment;
        }

        public IEnumerable<Establishment> GetPopular(int limit)
        {
            if (limit < 0) throw new EstablishmentException("Limit must not be less than 0");

            var list = _context.Establishment.Include(e => e.Store).Include(e => e.Address)
                .OrderByDescending(e => e.Visited).Take(limit);
            SetImgPathHostName(list);

            return list;
        }

        public IEnumerable<Establishment> GetForStore(int storeId)
        {
            var list = _context.Establishment.Include(e => e.Store).Include(e => e.Address)
                .Where(e => e.Store.StoreId == storeId);
            SetImgPathHostName(list);

            return list;
        }

        public IEnumerable<Establishment> GetForUser(int userId)
        {
            var list = _context.Establishment.Include(e => e.Store).Include(e => e.Address)
                .Where(e => e.SubscribedUsers.Any(ue => ue.UserId == userId)).Distinct();
            SetImgPathHostName(list);

            return list;
        }

        public void Delete(int id)
        {
            var establishment = _context.Establishment.Find(id);
            if (establishment != null)
            {
                _context.Establishment.Remove(establishment);
                _context.SaveChanges();
            }
        }

        #region Helpers

        private void SetImgPathHostName(IEnumerable<Establishment> list)
        {
            foreach (var establishment in list)
            {
                establishment.ImgPath = $"{AppSettings.HostName}{establishment.ImgPath}";
                if (establishment.Store != null)
                {
                    establishment.Store.ImgPath = $"{AppSettings.HostName}{establishment.Store.ImgPath}";
                }
            }
        }

        private void SetImgPathHostName(Establishment establishment)
        {
            establishment.ImgPath = $"{AppSettings.HostName}{establishment.ImgPath}";
            if (establishment.Store != null)
            {
                establishment.Store.ImgPath = $"{AppSettings.HostName}{establishment.Store.ImgPath}";
            }
        }

        #endregion
    }
}