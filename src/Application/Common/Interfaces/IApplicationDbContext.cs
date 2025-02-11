using SampleProject.Application.Admin.Queries.GetTasksPerUser;
using SampleProject.Domain.Entities;
using SampleProject.Domain.StoredProcedureObjects;

namespace SampleProject.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get;  }
    public DbSet<ApplicationRole> ApplicationRoles { get;  }
    public DbSet<ApplicationUserRole> ApplicationUserRoles { get;  }
    public DbSet<UserTask> Tasks { get; }
    public DbSet<Lookup> Lookups { get; }
    public DbSet<UserTaskCountDto> UserTaskCounts { get; } 

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
