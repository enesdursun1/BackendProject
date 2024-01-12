using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.Dtos.Requests.Category;
using Business.Dtos.Requests.Product;
using Business.Rules;
using Business.ValidationRules.FluentValidation;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Contexts;
using Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Business;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IProductService, ProductManager>();

        services.AddScoped<ICategoryService, CategoryManager>();

        services.AddScoped<ProductBusinessRules>();
        services.AddScoped<CategoryBusinessRules>();

       


   


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      
     


        return services;
    }


}