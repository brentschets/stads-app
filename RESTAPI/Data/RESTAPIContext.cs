using Microsoft.EntityFrameworkCore;
using RESTAPI.Models;

namespace RESTAPI.Data
{
    // ReSharper disable once InconsistentNaming
    public class RESTAPIContext : DbContext
    {
        public RESTAPIContext (DbContextOptions<RESTAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Store> Store { get; set; }

        public DbSet<Event> Event { get; set; }

        public DbSet<Promotion> Promotion { get; set; }
    }
}
