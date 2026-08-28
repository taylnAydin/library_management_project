using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Business.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Business
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
           services.AddScoped<IUserService, UserService>();
           services.AddScoped<IBookService, BookService>();
           services.AddScoped<IRentedLogService, RentedLogService>();
            return services;
        }
    }
}
