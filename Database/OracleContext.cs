using Aim.Core.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Database
{
    public class OracleContext:DbContext
    {
        public OracleContext()
        {

        }

        public OracleContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            if (!options.IsConfigured)
            {
                options.UseOracle(configuration.GetConnectionString("OracleContext"), options => options.UseOracleSQLCompatibility("11"));
            }
        }
        public DbSet<SubMeter> SUBMETER { get; set; }
        public DbSet<Subscriber> SUBSCRIBER { get; set; }
    }
}
