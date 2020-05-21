using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aspProekt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace aspProekt.Model
{
    public class AspProektContext : IdentityDbContext<IdentityUser>
    {
        public AspProektContext (DbContextOptions<AspProektContext> options)
            : base(options)
        {
        }

        public DbSet<aspProekt.Models.Student> Student { get; set; }

        public DbSet<aspProekt.Models.Course> Course { get; set; }

        public DbSet<aspProekt.Models.Teacher> Teacher { get; set; }

        public DbSet<aspProekt.Models.Enrollment> Enrollment { get; set; }
    }
}
