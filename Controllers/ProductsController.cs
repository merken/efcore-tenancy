using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efcore_tenancy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace efcore_tenancy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsDbContext context;
        public ProductsController(ProductsDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await context.Products.ToListAsync();
        }
    }
}
