using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using efcore_tenancy.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace efcore_tenancy.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDiscriminatorColumn(this IServiceCollection services, IConfiguration configuration)
        {
            var efServices = GetEfServices();
            efServices.AddScoped<IInterceptor, DiscriminatorColumnInterceptor>();

            return services.RegisterProductsDbContext(efServices,configuration);
        }

        public static IServiceCollection UseSchemaPerTenant(this IServiceCollection services, IConfiguration configuration)
        {
            var efServices = GetEfServices();
            efServices.AddScoped<IInterceptor, SchemaInterceptor>();

            return services.RegisterProductsDbContext(efServices,configuration);
        }

        public static IServiceCollection UseDatabasePerTenant(this IServiceCollection services, IConfiguration configuration)
        {
            var efServices = GetEfServices();
            efServices.AddScoped<IInterceptor, DatabaseInterceptor>();

            return services.RegisterProductsDbContext(efServices,configuration);
        }

        public static IServiceCollection UseConnectionPerTenant(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantInfoProvider, TenantInfoProvider>();

            services.AddScoped<ProductsDbContext>((serviceProvider) =>
            {
                var tenant = serviceProvider.GetRequiredService<ITenantInfoProvider>().GetTenantInfo();
                var connectionString = configuration.GetConnectionString(tenant.Name);
                var options = new DbContextOptionsBuilder<ProductsDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;
                var context = new ProductsDbContext(options);
                return context;
            });

            return services;
        }

        private static ServiceCollection GetEfServices()
        {
            var efServices = new ServiceCollection();

            efServices.AddEntityFrameworkSqlServer();
            efServices.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            efServices.AddScoped<ITenantInfoProvider, TenantInfoProvider>();

            return efServices;
        }

        private static IServiceCollection RegisterProductsDbContext(this IServiceCollection services, ServiceCollection efServices, IConfiguration configuration)
        {
            var efServiceProvider = efServices.BuildServiceProvider();
            var connectionString = configuration.GetConnectionString("SqlServerConnection");
            var dbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInternalServiceProvider(efServiceProvider)
                .UseSqlServer(connectionString)
                .Options;

            services.AddScoped<ProductsDbContext>((services) =>
                new ProductsDbContext(dbContextOptions));
            return services;
        }
    }
}