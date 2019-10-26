namespace MyDiabeticSystem.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyDiabeticSystem.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


    public class DataContext: IdentityDbContext<User>
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Ratio> Ratios { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Sensibility> Sensibilities { get; set; }

    }
}
