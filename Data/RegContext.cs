using DemoAuthorityProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAuthorityProject.Data
{
    public class RegContext : DbContext
    {
        public RegContext()
        {

        }

        public RegContext(DbContextOptions<RegContext> options): base(options)
        {

        }

        public DbSet<RegDTO> iProducts { get; set; }
    }
}
