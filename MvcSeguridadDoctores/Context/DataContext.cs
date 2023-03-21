using Microsoft.EntityFrameworkCore;
using MvcSeguridadDoctores.Models;

namespace MvcSeguridadDoctores.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Enfermo> Enfermos { get; set; }
    }
}
