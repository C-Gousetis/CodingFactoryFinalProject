using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using RandomNameGenerator.Domain;

namespace RandomNameGenerator.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Names> Names { get; set; }

        
    }
}
