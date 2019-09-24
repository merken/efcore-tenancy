
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace efcore_tenancy.Infrastructure
{
    public interface ITenantInfoProvider
    {
        TenantInfo GetTenantInfo();
    }
    public class TenantInfoProvider : ITenantInfoProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TenantInfoProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public TenantInfo GetTenantInfo()
        {
            return httpContextAccessor.HttpContext.RequestServices.GetRequiredService<TenantInfo>();
        }
    }
}