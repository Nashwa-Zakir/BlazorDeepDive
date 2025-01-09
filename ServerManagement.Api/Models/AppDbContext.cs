using Microsoft.EntityFrameworkCore;
using ServerManagement.Models;

namespace ServerManagement.Api.Models
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Server> Servers { get; set; }
    }
}
