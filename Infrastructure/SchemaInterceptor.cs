using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace efcore_tenancy.Infrastructure
{
    public class SchemaInterceptor : DbCommandInterceptor
    {
        private readonly TenantInfo tenantInfo;
        public SchemaInterceptor(TenantInfo tenantInfo)
        {
            this.tenantInfo = tenantInfo;
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, 
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result, 
            CancellationToken cancellationToken = default)
        {
            command.CommandText = $"USE TenantPerSchemaDb {command.CommandText}";
            command.CommandText = command.CommandText
                .Replace("FROM ", $" FROM {tenantInfo.Name}.")
                .Replace("JOIN ", $" JOIN {tenantInfo.Name}.")
            ;

            return base.ReaderExecutingAsync(command, eventData, result);
        }
    }
}