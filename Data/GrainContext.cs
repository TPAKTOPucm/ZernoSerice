using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zerno.Models;

namespace Zerno.Data
{
    public class GrainContext : DbContext
    {
        public GrainContext(DbContextOptions<GrainContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }
    }
}