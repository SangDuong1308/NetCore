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
        public ApplicationDBContext()
        {
            
        }

        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>().ToTable("Books");
            builder.Entity<Category>().ToTable("Categories").HasMany(c => c.Books)
                .WithOne(e => e.Category).HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

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