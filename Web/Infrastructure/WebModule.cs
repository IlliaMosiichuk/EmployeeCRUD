using AppCore.Validators;
using Autofac;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();

            builder.RegisterType<AppDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<EmployeeRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EfUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeValidator>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeImportService>().AsImplementedInterfaces().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}