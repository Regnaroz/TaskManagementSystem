using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Common.Mappings;
using SampleProject.Application.Common.Models;
using SampleProject.Application.Tasks.Common;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Enums;
namespace SampleProject.Application.Tasks.Queries.GetTasks;
public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, ErrorOr<PaginatedList<TaskDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public GetTasksQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IApplicationDbContext context, IUser user)
    {
        _mapper = mapper;
        _context = context;
        _user = user;
    }

    public async Task<ErrorOr<PaginatedList<TaskDto>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var query =  _context.Tasks.AsNoTracking()
                                  .AsSplitQuery()
                                  .Where(t => request.isAdmin ? true : t.UserId == _user.Id
                                              && string.IsNullOrEmpty(request.filter.title) ? true : t.Title.ToLower().Contains(request.filter.title!.ToLower())
                                              && request.filter.Status == null ? true : t.Status == (Status)request.filter.Status!
                                              && request.filter.PriorityLevel == null ? true : t.Priority == (PriorityLevel)request.filter.PriorityLevel!
                                              && request.filter.duoDate == null ? true : t.DueDate <= request.filter.duoDate
                                       );

       query = HandleOrderBy(query,request.filter);

       var result  = await query.ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                                .PaginatedListAsync(request.filter.PageNumber, request.filter.PageSize);


        return result;
    }

    private IQueryable<UserTask> HandleOrderBy(IQueryable<UserTask> query, TasksFilter filter)
    {
        switch (filter.orderBy) {

            case TasksOrderByEnum.Priority:
                return filter.isDesc ? query.OrderByDescending(t => t.Priority) : query.OrderBy(t => t.Priority);

            case TasksOrderByEnum.DuoDate:
                return filter.isDesc ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate);

            default:
                return filter.isDesc ? query.OrderByDescending(t => t.Created) : query.OrderBy(t => t.Created);

        }
    }
}
