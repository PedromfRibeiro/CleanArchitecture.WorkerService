using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Entities.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchitecture.Infrastructure.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public required DbSet<ApiException> DbApiException { get; set; }

    public required DbSet<LogEntry> DbLogEntry { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
}
