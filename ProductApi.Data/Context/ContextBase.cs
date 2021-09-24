using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Data.Context
{
    public class ContextBase<TContext> : DbContext where TContext : DbContext
    {
        protected ContextBase(DbContextOptions<TContext> dbContext)
            : base(dbContext)
        {
        }
    }
}
