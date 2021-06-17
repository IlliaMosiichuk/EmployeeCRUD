using AppCore.Entities;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContextSeed  : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            try
            {
                var employees = new[]
            {
                new Employee
                {
                    FirstName = "FName1",
                    LastName = "LName1",
                    MiddleName = "MName1",
                    Gender = Gender.Male,
                    BirthDate = DateTime.Now.AddYears(-30),
                    IsExternal = false,
                    Number = "1"
                },
                new Employee
                {
                    FirstName = "FName2",
                    LastName = "LName2",
                    MiddleName = "MName2",
                    Gender = Gender.Female,
                    BirthDate = DateTime.Now.AddYears(-31),
                    IsExternal = false,
                    Number = "2",
                },
                new Employee
                {
                    FirstName = "FName1",
                    LastName = "LName1",
                    MiddleName = "MName1",
                    Gender = Gender.Male,
                    BirthDate = DateTime.Now.AddYears(-33),
                    IsExternal = true,
                },
            };

                context.Employees.AddRange(employees);

                base.Seed(context);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
