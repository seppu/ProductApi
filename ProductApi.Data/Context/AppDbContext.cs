using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductApi.Core.Model;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cascade delete behaviour will prevent a table with FK to have temporal table support
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                .SelectMany(t => t.GetForeignKeys())
                                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name);
                entity.ToTable("Products");
            });
        }
    }
}
