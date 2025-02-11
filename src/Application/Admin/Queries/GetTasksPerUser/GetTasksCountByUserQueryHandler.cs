using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Entities;
using SampleProject.Domain.StoredProcedureObjects;
namespace SampleProject.Application.Admin.Queries.GetTasksPerUser;
public class GetTasksCountByUserQueryHandler : IRequestHandler<GetTasksCountByUserQuery, ErrorOr<List<UserTaskCountDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTasksCountByUserQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    async Task<ErrorOr<List<UserTaskCountDto>>> IRequestHandler<GetTasksCountByUserQuery, ErrorOr<List<UserTaskCountDto>>>.Handle(GetTasksCountByUserQuery request, CancellationToken cancellationToken)
    {

        //var tasks = _context.Tasks
        //   .FromSqlRaw("EXEC GetTasksByStatus @p0, @p1", 1, "2cba278c-4d40-4378-9820-7000c0892918" )
        //   .ToList();

        //var tasks2 = await _context.Tasks
        //.FromSqlRaw("EXEC GetTasksDueToday")
        //.ToListAsync();

        var userGroupedTasks = await _context.UserTaskCounts
                    .FromSqlRaw("EXEC GetUserTaskCounts")
                    .ToListAsync();

        return userGroupedTasks;
    }
}
