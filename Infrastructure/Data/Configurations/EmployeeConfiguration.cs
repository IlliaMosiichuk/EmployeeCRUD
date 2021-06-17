using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            this.HasKey(e => e.Id);

            this.Property(e => e.FirstName)
                .IsRequired();

            this.Property(e => e.MiddleName)
                .IsRequired();

            this.Property(e => e.LastName)
                .IsRequired();

            //this.Property(e => e.Number)
            //    .IsRequired();
        }
    }
}
