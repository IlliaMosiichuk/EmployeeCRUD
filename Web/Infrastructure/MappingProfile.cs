using AppCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Employees;

namespace Web.Infrastructure
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeItemViewModel>();
            CreateMap<Employee, EmployeeDetailsViewModel>();
            CreateMap<Employee, EmployeeManageViewModel>();
            CreateMap<EmployeeManageViewModel, Employee>()
                .ForMember(m => m.Number, opt => opt.Ignore())
                .ForMember(m => m.IsExternal, opt => opt.Ignore());
        }
    }
}