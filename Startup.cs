using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efcore_tenancy.Data;
using efcore_tenancy.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace efcore_tenancy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<TenantInfo>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var efServices = new ServiceCollection();
            efServices.AddEntityFrameworkSqlServer();
            efServices.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            efServices.AddScoped<ITenantInfoProvider, TenantInfoProvider>();
            // efServices.AddScoped<IInterceptor, DiscriminatorColumnInterceptor>();
            // efServices.AddScoped<IInterceptor, DatabaseInterceptor>();
            efServices.AddScoped<IInterceptor, SchemaInterceptor>();

            var efServiceProvider = efServices.BuildServiceProvider();
            var connectionString = Configuration.GetConnectionString("SqlServerConnection");
            var dbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInternalServiceProvider(efServiceProvider)
                .UseSqlServer(connectionString)
                .Options;

            services.AddScoped<ProductsDbContext>((services) =>
                new ProductsDbContext(dbContextOptions));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<TenantInfoMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
