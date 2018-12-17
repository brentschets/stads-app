using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Exceptions;
using RESTAPI.Models;
using RESTAPI.Utils;

namespace RESTAPI.Repositories
{
    public interface IPromotionRepository
    {
        IEnumerable<Promotion> GetAll();
        IEnumerable<Promotion> GetPopular(int limit);
        IEnumerable<Promotion> GetForStore(int storeId);
        IEnumerable<Promotion> GetForEstablishment(int establishmentId);
    }

    public class PromotionRepository : IPromotionRepository
    {
        private readonly RESTAPIContext _context;

        public PromotionRepository(RESTAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Promotion> GetAll()
        {
            var list = _context.Promotion.Include(p => p.Store);
            SetImgPathHostName(list);
            return list;
        }

        public IEnumerable<Promotion> GetPopular(int limit)
        {
            if (limit < 0) throw new PromotionException("Limit must not be less than 0");

            var list = _context.Promotion.Include(p => p.Store).OrderByDescending(p => p.Visited).Take(limit);
            SetImgPathHostName(list);

            return list;
        }

        public IEnumerable<Promotion> GetForStore(int storeId)
        {
            var list = _context.Promotion.Include(p => p.Store).Where(p => p.Store.StoreId == storeId);
            SetImgPathHostName(list);

            return list;
        }

        public IEnumerable<Promotion> GetForEstablishment(int establishmentId)
        {
            var list = _context.Promotion.Include(p => p.Store).ThenInclude(s => s.Establishments).Where(p =>
                p.Store.Establishments.Any(e => e.EstablishmentId == establishmentId));
            SetImgPathHostName(list);

            return list;
        }

        #region Helpers

        private void SetImgPathHostName(IEnumerable<Promotion> list)
        {
            foreach (var promotion in list)
            {
                if (promotion.Store != null)
                {
                    promotion.Store.ImgPath = $"{AppSettings.HostName}{promotion.Store.ImgPath}";

                    if (promotion.Store.Establishments != null)
                    {
                        foreach (var establishment in promotion.Store.Establishments)
                        {
                            establishment.ImgPath = $"{AppSettings.HostName}{establishment.ImgPath}";
                        }
                    }
                }
            }
        }

        #endregion
    }
}