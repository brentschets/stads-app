using Microsoft.EntityFrameworkCore;
using RESTAPI.Models;

namespace RESTAPI.Data
{
    public class RESTAPIContext : DbContext
    {
        public RESTAPIContext (DbContextOptions<RESTAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Store> Store { get; set; }
    }
}
