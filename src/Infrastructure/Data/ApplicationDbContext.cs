using System.Reflection;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Admin.Queries.GetTasksPerUser;
using SampleProject.Domain.StoredProcedureObjects;

namespace SampleProject.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
    IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
    public DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();
    public DbSet<UserTask> Tasks => Set<UserTask>();
    public DbSet<Lookup> Lookups => Set<Lookup>();
    public DbSet<UserTaskCountDto> UserTaskCounts  => Set<UserTaskCountDto>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(b =>
        {
            b.HasMany(e => e.ApplicationUserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            b.Property(u => u.isActive).
              HasDefaultValue(true);
        }) ;

        builder.Entity<ApplicationRole>(b =>
        { 
            b.HasMany(e => e.ApplicationUserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

        builder.Entity<UserTaskCountDto>().HasNoKey();
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
