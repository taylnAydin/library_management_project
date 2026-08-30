using LibraryManagement.API.Services.Abstract;
using LibraryManagement.API.Services.Concrete;

namespace LibraryManagement.API
{
    public static class DependcyInjection //niye static niye this niye iservicecollection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            return services;
        }

    }
}
