namespace MyDiabeticSystem.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyDiabeticSystem.Web.Data.Entities;

    public class DataContext:DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Check> Checks { get; set; }
        
    }
}
