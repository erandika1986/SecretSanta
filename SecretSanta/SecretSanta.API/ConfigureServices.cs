using Microsoft.EntityFrameworkCore;
using SecretSanta.API.Data;
using SecretSanta.API.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<SecretSantaContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure(2));
            });

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
