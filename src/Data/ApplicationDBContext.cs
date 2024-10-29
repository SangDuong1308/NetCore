using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Name = "HRStaff",
                    NormalizedName = "HRSTAFF"
                },
                new IdentityRole
                {
                    Name = "HRHead",
                    NormalizedName = "HRHEAD"
                },
                new IdentityRole
                {
                    Name = "DeptHead",
                    NormalizedName = "DEPTHEAD"
                },
                new IdentityRole
                {
                    Name = "Ceo",
                    NormalizedName = "CEO"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}