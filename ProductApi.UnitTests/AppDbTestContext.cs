using Microsoft.EntityFrameworkCore;
using ProductApi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.UnitTests
{
    public class AppDbTestContext : AppDbContext
    {
        public AppDbTestContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }
    }
}
