using LibraryManagement.Core.Interfaces;
using LibraryManagement.DataAccess.Context;
using LibraryManagement.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess
{
    // niye static extension method ne 
    public static class DependencyInjection
    {  
        // niye Iservice niye this niye ikinci parametre Iconfiguration
         public static IServiceCollection AddDataAccessService(this IServiceCollection services, IConfiguration configuration)
        {
            //null dönmesin diye
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection bulunamadı.");

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            //addscoped ve iki tane typeof niye , interface ve class niye ?
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRentedLogRepository, RentedLogRepository>();

            return services;
        }

    }
}
