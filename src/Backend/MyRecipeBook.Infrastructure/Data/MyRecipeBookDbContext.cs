using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

[assembly: InternalsVisibleTo("MyRecipeBook.IntegrationTests")]
namespace MyRecipeBook.Infrastructure.Data;

internal class MyRecipeBookDbContext(DbContextOptions<MyRecipeBookDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}