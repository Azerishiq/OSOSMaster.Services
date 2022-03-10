using Aim.Core.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Database
{
    public class AimContext :DbContext
    {
        public AimContext(DbContextOptions<AimContext> options) : base(options)
        {
        }
        public DbSet<CustomerCounter> CustomerCounter { get; set; }
        public DbSet<ModemList> ModemList { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<MeterPower> MeterPowers { get; set; }
        public DbSet<MetersCategory> MetersCategories { get; set; }
        public DbSet<NodeMeter> NodeMeters { get; set; }
        public DbSet<ReadingReadout> ReadingReadout { get; set; }
        public DbSet<ReadingReadoutExport> ReadingReadoutExport { get; set; }
        public DbSet<ReadingReadoutReactive> ReadingReadoutReactive { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<CustomerCounter>(builder =>
                {
                    builder.HasNoKey();
                    builder.ToTable("CustomerCounter");
                });
            modelBuilder
                .Entity<ModemList>(builder =>
                {
                    builder.HasNoKey();
                    builder.ToTable("ModemList");
                });
            modelBuilder
               .Entity<Meter>(builder =>
               {
                   builder.HasNoKey();
                   builder.ToTable("Meter");
               });
            modelBuilder
               .Entity<ReadingReadout>(builder =>
               {
                   builder.HasNoKey();
                   builder.ToTable("ReadingReadout");
               });
            modelBuilder
               .Entity<ReadingReadoutExport>(builder =>
               {
                   builder.HasNoKey();
                   builder.ToTable("ReadingReadoutExport");
               });
            modelBuilder
               .Entity<ReadingReadoutReactive>(builder =>
               {
                   builder.HasNoKey();
                   builder.ToTable("ReadingReadoutReactive");
               });
        }
    }
}
