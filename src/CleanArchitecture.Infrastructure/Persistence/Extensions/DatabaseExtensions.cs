using CleanArchitecture.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Persistence.Extensions;

public static class DatabaseExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DataContext>();
        services.AddDbContext<DataContext>(opt =>
            opt.UseSqlServer(configuration.GetExpectedValue<string>("Configs:Database:DefaultConnection"),
                options => options.MigrationsAssembly("Cashflow.Persistence")
                )
            );
    }
}
