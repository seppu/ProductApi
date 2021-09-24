using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProductApi.Data.Context
{
    public class AppDbContext : ContextBase<AppDbContext>
    {
        private readonly ILogger _logger;
        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger = null)
            : base(options)
        {
            _logger = logger;
        }
    }
}
