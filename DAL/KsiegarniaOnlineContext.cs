using KsiegarniaOnline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiegarniaOnline.DAL
{
    public class KsiegarniaOnlineContext : DbContext
    {
        public KsiegarniaOnlineContext(DbContextOptions<KsiegarniaOnlineContext> options)
         : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<BookCategory>().ToTable("BookCategories");
            modelBuilder.Entity<Profile>().ToTable("Profiles");
        }

    }
}
