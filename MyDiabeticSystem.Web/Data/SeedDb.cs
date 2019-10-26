using MyDiabeticSystem.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDoctorsAsync();
            await CheckPatientsAsync();
        }

        private async Task CheckDoctorsAsync()
        {
            if(!_context.Doctors.Any())
            {
                AddDoctor("123456789", "Ramon", "Gamboa", "22222222");
                AddDoctor("345456791", "Julian", "Martinez", "8888888");
                AddDoctor("566789801", "Carlota", "Ruiz", "999999999");
                await _context.SaveChangesAsync();
            }
        }

        private void AddDoctor(string document, 
            string firstName, 
            string lastName, 
            string cellPhone)
        {
            _context.Doctors.Add(new Doctor
            {
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                LastName = lastName
            });
        }

        private async Task CheckPatientsAsync()
        {
            if (!_context.Patients.Any())
            {
                AddPatient("123456789", "Maria", "Salcedo", "22222222", DateTime.Parse("04/18/2019"));
                AddPatient("345456791", "Karla", "Martinez", "8888888", DateTime.Parse("04/18/1999"));
                AddPatient("566789801", "Juan", "Perez", "999999999", DateTime.Parse("04/18/1980"));
                await _context.SaveChangesAsync();
            }
        }
        private void AddPatient(string document, 
            string firstName, 
            string lastName, 
            string cellPhone, 
            DateTime dateBirth)
        {
            _context.Patients.Add(new Patient
            {
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                LastName = lastName,
                DateBirth=dateBirth,
                CanEdit=false
            });
        }

    }
}
