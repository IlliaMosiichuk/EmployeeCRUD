using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Employees
{
    public class EmployeeDetailsViewModel
    {
        public Guid Id { get; set; }

        public bool IsEditMode { get; set; }

        public string Number { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsExternal { get; set; }
    }
}