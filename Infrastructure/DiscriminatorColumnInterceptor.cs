using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace efcore_tenancy.Infrastructure
{
    public class DiscriminatorColumnInterceptor : DbCommandInterceptor
    {
        private readonly ITenantInfoProvider tenantInfoProvider;
        public DiscriminatorColumnInterceptor(ITenantInfoProvider tenantInfoProvider)
        {
            this.tenantInfoProvider = tenantInfoProvider;
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            var tenantInfo = tenantInfoProvider.GetTenantInfo();
            command.CommandText = $"USE DiscriminatorDB {command.CommandText}";

            if (command.CommandText.Contains("WHERE"))
            {
                command.CommandText += $" AND Tenant = '{tenantInfo.Name}'";
            }
            else
            {
                command.CommandText += $" WHERE Tenant = '{tenantInfo.Name}'";
            }

            return base.ReaderExecutingAsync(command, eventData, result);
        }
    }
}