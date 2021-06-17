using AppCore.Entities;
using AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class EmployeeRepository : EfRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context)
            : base(context)
        {
        }

        public bool Contains(string number)
        {
            return _set.Any(e => e.Number == number);
        }
    }
}
