using System;
using System.Collections.Generic;
using System.Text;
using Gateways.Repository.Domain;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Repository.Context
{
    public class GatewayContext : DbContext
    {
        public GatewayContext(DbContextOptions<GatewayContext> options) : base(options)
        {

        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
