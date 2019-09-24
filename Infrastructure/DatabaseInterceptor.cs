using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace efcore_tenancy.Infrastructure
{
    public class DatabaseInterceptor : DbCommandInterceptor
    {
        private readonly ITenantInfoProvider tenantInfoProvider;
        public DatabaseInterceptor(ITenantInfoProvider tenantInfoProvider)
        {
            this.tenantInfoProvider = tenantInfoProvider;
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            var tenantInfo = tenantInfoProvider.GetTenantInfo();

            command.CommandText = $"USE {tenantInfo.Name}DB {command.CommandText}";

            return base.ReaderExecutingAsync(command, eventData, result);
        }
    }
}