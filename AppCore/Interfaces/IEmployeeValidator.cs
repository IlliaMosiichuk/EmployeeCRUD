﻿using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface IEmployeeValidator
    {
        List<string> GetValidationErrors(Employee employee);
    }
}
