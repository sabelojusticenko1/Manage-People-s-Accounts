using ManagePeople.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagePeople.ManangeDbContext
{
    public class ManageDbContext : DbContext
    {
    public ManageDbContext(DbContextOptions<ManageDbContext> options)
           : base(options)
        {
        }

        public DbSet<Person> TbPerson { get; set; }
        public DbSet<Accounts> TbAccounts { get; set; }
        public DbSet<Transations> TbTransations { get; set; }
    }
}
