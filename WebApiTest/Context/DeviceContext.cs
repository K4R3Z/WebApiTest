using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.Context
{
    public class DeviceContext : DbContext
    {
        public DeviceContext() { }

        public DeviceContext (DbContextOptions<DeviceContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=devices.db");
    }
}
