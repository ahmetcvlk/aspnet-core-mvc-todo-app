using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        


    }
}
