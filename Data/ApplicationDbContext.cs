using Microsoft.EntityFrameworkCore;
using TodoWebApp.Models;

namespace TodoWebApp.Data
{
    public class ApplicationDbContext(DbContextOptions op) : DbContext(op)
    {
        public DbSet<Todo> Todos { get; set; } = null!;
    }
}
