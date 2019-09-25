using efcore_tenancy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace efcore_tenancy.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDiscriminatorColumn(this IServiceCollection services, IConfiguration configuration)
         => services.UseEFInterceptor<DiscriminatorColumnInterceptor>(configuration);

        public static IServiceCollection UseSchemaPerTenant(this IServiceCollection services, IConfiguration configuration)
         => services.UseEFInterceptor<SchemaInterceptor>(configuration);

        public static IServiceCollection UseDatabasePerTenant(this IServiceCollection services, IConfiguration configuration)
         => services.UseEFInterceptor<DatabaseInterceptor>(configuration);
        
        private static IServiceCollection UseEFInterceptor<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class, IInterceptor
        {
            return services
                .AddScoped<DbContextOptions>((serviceProvider) =>
                    {
                        var tenant = serviceProvider.GetRequiredService<TenantInfo>();

                        var efServices = new ServiceCollection();
                        efServices.AddEntityFrameworkSqlServer();
                        efServices.AddScoped<TenantInfo>(s => 
                            serviceProvider.GetRequiredService<TenantInfo>()); // Allows DI for tenant info, set by parent pipeline via middleware
                        efServices.AddScoped<IInterceptor, T>(); // Adds the interceptor

                        var connectionString = configuration.GetConnectionString("SqlServerConnection");

                        return new DbContextOptionsBuilder<ProductsDbContext>()
                            .UseInternalServiceProvider(efServices.BuildServiceProvider())
                            .UseSqlServer(connectionString)
                            .Options;
                    })
                .AddScoped<ProductsDbContext>(s => new ProductsDbContext(s.GetRequiredService<DbContextOptions>()));
        }

        public static IServiceCollection UseConnectionPerTenant(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ProductsDbContext>((serviceProvider) =>
            {
                var tenant = serviceProvider.GetRequiredService<TenantInfo>(); // Get from parent service provider (ASP.NET MVC Pipeline)
                var connectionString = configuration.GetConnectionString(tenant.Name);
                var options = new DbContextOptionsBuilder<ProductsDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;
                var context = new ProductsDbContext(options);
                return context;
            });

            return services;
        }
    }
}