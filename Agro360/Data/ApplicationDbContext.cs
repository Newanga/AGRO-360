using Agro360.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Harvest> HarvestType { get; set; }

        public DbSet<Report> FarmerReport { get; set; }

        public DbSet<Inquiry> Inquiry { get; set; }
    }
}
