using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet3.Data;
using Autofac;
using Microsoft.Extensions.Logging;

namespace aspnet3.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           

            builder.RegisterType<ApplicationDbContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
