using AppCore.Entities;
using AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Validators
{
    public class EmployeeValidator : IEmployeeValidator
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<string> GetValidationErrors(Employee employee)
        {
            var validationErrors = new List<string>();

            if (employee.IsExternal && !string.IsNullOrEmpty(employee.Number))
            {
                validationErrors.Add("Number must be empty for external Employee");
            }
            else if (!employee.IsExternal)
            {
                if (string.IsNullOrEmpty(employee.Number))
                {
                    validationErrors.Add("Number cannot be empty for not external Employee");
                }
                else
                {
                    var hasEmployeeWithNumber = _employeeRepository.Contains(employee.Number);
                    if (hasEmployeeWithNumber)
                        validationErrors.Add("Number must be unique");
                }
            }

            return validationErrors;
        }
    }
}
