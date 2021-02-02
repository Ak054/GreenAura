using System;
using System.Collections.Generic;
using System.Text;
using GreenAura.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenAura.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<State> States { get; set; }
        public DbSet<PlantType> PlantTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
    }
}
