using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestProject.Models;

namespace UserTestProject.Models
{
    public class CoreAngularContext : DbContext
    {
        public CoreAngularContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }


}
