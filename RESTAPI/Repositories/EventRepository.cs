using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Exceptions;
using RESTAPI.Models;
using RESTAPI.Utils;
using System.Collections.Generic;
using System.Linq;

namespace RESTAPI.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        IEnumerable<Event> GetPopular(int limit);
        IEnumerable<Event> GetForEstablishment(int establishmentId);
    }

    public class EventRepository : IEventRepository
    {
        private readonly RESTAPIContext _context;

        public EventRepository(RESTAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAll()
        {
            var list = _context.Event.Include(e => e.Establishment).ThenInclude(e => e.Address);
            SetImgPathHostName(list);
            return list;
        }

        public IEnumerable<Event> GetPopular(int limit)
        {
            if (limit < 0) throw new EventException("Limit must not be less than 0");

            var list = _context.Event.Include(e => e.Establishment).ThenInclude(e => e.Store)
                .Include(e => e.Establishment)
                .ThenInclude(e => e.Address).OrderByDescending(e => e.Visited).Take(limit);
            SetImgPathHostName(list);

            return list;
        }

        public IEnumerable<Event> GetForEstablishment(int establishmentId)
        {
            var list = _context.Event.Where(e => e.Establishment.EstablishmentId == establishmentId);
            SetImgPathHostName(list);

            return list;
        }

        #region Helpers

        private void SetImgPathHostName(IEnumerable<Event> list)
        {
            foreach (var @event in list)
            {
                if (@event.Establishment != null)
                {
                    @event.Establishment.ImgPath = $"{AppSettings.HostName}{@event.Establishment.ImgPath}";

                    if (@event.Establishment.Store != null)
                    {
                        @event.Establishment.Store.ImgPath =
                            $"{AppSettings.HostName}{@event.Establishment.Store.ImgPath}";
                    }
                }
            }
        }

        #endregion
    }
}