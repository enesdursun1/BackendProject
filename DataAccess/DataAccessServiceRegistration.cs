using Application.Services.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public static class DataAccessServiceRegistration
{

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
      
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BackendProjectConnectionString")));

        services.AddScoped<IProductDal, EfProductDal>();
        services.AddScoped<ICategoryDal, EfCategoryDal>();
       
        services.AddScoped<IUserDal, EfUserDal>();
        services.AddScoped<IOperationClaimDal, EfOperationClaimDal>();
        services.AddScoped<IUserOperationClaimDal, EfUserOperationClaimDal>();
        services.AddScoped<IRefreshTokenDal, EfRefreshTokenDal>();
       


        return services;
    }
}
