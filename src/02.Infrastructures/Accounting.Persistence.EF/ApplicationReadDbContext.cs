using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Accounting.Persistence.EF;

public class ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options) :
    IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .ApplyConfigurationsFromAssembly(
                GetType().Assembly);
    }

    public override int SaveChanges()
    {
        throw new Exception("can not use application read db context for save data");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new Exception("can not use application read db context for save data");
    }

    public override ChangeTracker ChangeTracker
    {
        get
        {
            var tracker = base.ChangeTracker;
            tracker.LazyLoadingEnabled = false;
            tracker.AutoDetectChangesEnabled = false;
            tracker.QueryTrackingBehavior =
                QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            return tracker;
        }
    }
}