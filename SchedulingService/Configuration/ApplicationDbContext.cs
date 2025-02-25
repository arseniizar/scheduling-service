using Microsoft.EntityFrameworkCore;
using SchedulingService.Models;

namespace SchedulingService.Configuration;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<ScheduledTask> ScheduledTasks { get; set; }
}