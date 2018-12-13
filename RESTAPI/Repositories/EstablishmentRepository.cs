using System.Collections.Generic;
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
        IEnumerable<Establishment> GetPopular(int limit);
        IEnumerable<Establishment> GetForStore(int storeId);
        IEnumerable<Establishment> GetForUser(int userId);
    }

    public class EstablishmentRepository : IEstablishmentRepository
    {
        private readonly RESTAPIContext _context;

        public EstablishmentRepository(RESTAPIContext context)
        {
            _context = context;
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

        #endregion
    }
}