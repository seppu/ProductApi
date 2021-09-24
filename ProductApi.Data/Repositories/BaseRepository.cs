using Microsoft.EntityFrameworkCore;
using ProductApi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }
    }
}
