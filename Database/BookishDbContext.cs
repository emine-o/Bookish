namespace Bookish.Database;
using Microsoft.EntityFrameworkCore;
using Bookish.Models;

public class BookishDbContext : DbContext
{
    public BookishDbContext(): base() {}
    public DbSet<Book> Book { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<User> User { get; set; }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Book>().Property(c => c.Title)
    //     .UseCollation("SQL_Latin1_General_CP1_CI_AS");
    //     modelBuilder.Entity<Book>().Property(c => c.Author)
    //     .UseCollation("SQL_Latin1_General_CP1_CI_AS");
    // }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=bookish;User Id=bookish;Password=bookish;Include Error Detail=true");
    }
}