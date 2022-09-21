using Microsoft.EntityFrameworkCore;
using MT.PersonService.Core.Entity;
using MT.PersonService.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.PersonService.Data.Contexts
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

           

            modelBuilder.ApplyConfiguration(new PersonSeed(new int[] { 1, 2, 3 }));
            modelBuilder.ApplyConfiguration(new ContactInfoSeed(new int[] { 1, 2, 3 }));
        }
    }
}
