using Gui.Core.Domain.Units;
using Gui.Core.Domain.Users;
using Gui.Core.SharedKernel;
using Gui.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gui.Infrastructure.Persistence;

public class ApplicationContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public ApplicationContext(DbContextOptions configuration, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(configuration)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor ?? throw new ArgumentNullException(nameof(publishDomainEventsInterceptor));
    }

    public DbSet<User> Userss { get; set; } = null!;
    public DbSet<UserRoleAssignment> UserRoleAssignments { get; set; } = null!;
    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<Operator> Operators { get; set; } = null!;
    public DbSet<Observer> Observers { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRoleAssignment>(entity =>
        {
            entity.HasKey(ura => ura.Id);
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.HasKey(o => new { o.UserId, o.UnitId });
        });

        modelBuilder.Entity<Observer>(entity =>
        {
            entity.HasKey(o => new { o.UserId, o.UnitId });
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
