using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.EagerLazyLoading
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Review> Reviews { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }
        public Book Book { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(b => b.BookId);

            var books = Enumerable.Range(1, 10).Select(i => new Book { Id = i, Title = $"Book {i}", Reviews = new List<Review>() }).ToList();
            for (int i = 1; i <= 100; i++)
            {
                books[(i - 1) % 10].Reviews.Add(new Review { Id = i, BookId = (i - 1) % 10 + 1, Comment = $"Review {i}" });
            }

            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Review>().HasData(books.SelectMany(b => b.Reviews));
        }

    }
}
