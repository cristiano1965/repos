using Microsoft.EntityFrameworkCore; // UseSqlServer
using Microsoft.Extensions.DependencyInjection; // IServiceCollection

namespace Packt.Shared;

public static class NorthwindContextExtensions
{
    /// <summary>
    /// Adds NorthwindContext to the specified IServiceCollection. Uses the SqlServer database provider.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString">Set to override the default.</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static IServiceCollection AddNorthwindContext(
      this IServiceCollection services, string connectionString =
        "Data Source=.;Initial Catalog=Northwind;"
        + "Integrated Security=true;MultipleActiveResultsets=true;")
    {
        services.AddDbContext<NorthwindContext>(options =>
          options.UseSqlServer(connectionString)
          .UseLoggerFactory(new ConsoleLoggerFactory()), ServiceLifetime.Transient  //senza il transient da un errore che dice "InvalidOperationException: A second operation started on this context before a previous operation completed."
        );

        return services;
    }
}
