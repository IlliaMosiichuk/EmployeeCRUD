using AppCore.Entities;
using AppCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmployeeImportService : IEmployeeImportService
    {
        private readonly IEmployeeValidator _employeeValidator;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeImportService(
            IEmployeeValidator employeeValidator,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeValidator = employeeValidator;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<string>> Import(Stream stream)
        {
            var parsedEmployees = Parse(stream);

            var results = new List<string>();
            var createdEmployees = new List<Employee>();
            foreach (var employee in parsedEmployees)
            {
                string result;
                var validationErrors = _employeeValidator.GetValidationErrors(employee);
                if (validationErrors.Any())
                {
                    result = $"{parsedEmployees.IndexOf(employee)} - Failed - {string.Join(";", validationErrors)}";
                }
                else if (createdEmployees.Any(e => e.Number == employee.Number))
                {
                    result = $"{parsedEmployees.IndexOf(employee)} - Failed - Duplicate employee";
                }
                else
                {
                    createdEmployees.Add(employee);
                    result = $"{parsedEmployees.IndexOf(employee)} - Created";
                }

                results.Add(result);
            }

            if (createdEmployees.Any())
            {
                _employeeRepository.AddRange(createdEmployees);
                await _unitOfWork.CommitAsync();
            }

            return results;
        }

        private List<Employee> Parse(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var jsonSerializer = new JsonSerializer();
                var employees = (List<Employee>)jsonSerializer.Deserialize(streamReader, typeof(List<Employee>));
                return employees;
            }
        }
    }
}
