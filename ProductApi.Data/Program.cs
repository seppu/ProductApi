using ProductApi.Data.Context;
using System;

namespace ProductApi.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            var contextFactory = new AppDbContextFactory();
            var context = contextFactory.CreateDbContext(new string[0]);
        }
    }
}
