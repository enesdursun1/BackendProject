using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public static class CoreModule
    {

        public static void AddDependencyResolvers(this IServiceCollection services)
        {

          

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<LoggerServiceBase, FileLogger>();


        }


    }
}
