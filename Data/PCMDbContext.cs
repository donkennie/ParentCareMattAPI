using Microsoft.EntityFrameworkCore;
using ParentCareMatts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParentCareMatts.Data
{
    public class PCMDbContext:DbContext
    {
        public PCMDbContext(DbContextOptions<PCMDbContext> options) : base(options)
        {

        }

        public DbSet<Registration> GetRegistrations { get; set; }
        public DbSet<Login> GetLogins{ get; set; }
        public DbSet<FamilyDetails> GetFamilies { get; set; }

    }
}
