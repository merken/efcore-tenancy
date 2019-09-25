using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace efcore_tenancy.Infrastructure
{
    public class DatabaseInterceptor : DbCommandInterceptor
    {
        private readonly TenantInfo tenantInfo;
        public DatabaseInterceptor(TenantInfo tenantInfo)
        {
            this.tenantInfo = tenantInfo;
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, 
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            command.CommandText = $"USE {tenantInfo.Name}DB {command.CommandText}";

            return base.ReaderExecutingAsync(command, eventData, result);
        }
    }
}