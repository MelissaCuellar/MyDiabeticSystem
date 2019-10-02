namespace MyDiabeticSystem.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyDiabeticSystem.Web.Data.Entities;

    public class DataContext:DbContext
    {
        public DbSet<Check> Checks { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

    }
}
