using ErrorOr;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Tasks.Common;
namespace SampleProject.Application.Tasks.Queries.GetTaskByStatusOrUserId;
public class GetTaskByIdQueryHandler
    : IRequestHandler<GetTaskByStatusOrUserIdQuery, ErrorOr<List<TaskDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ErrorOr<List<TaskDto>>> Handle(GetTaskByStatusOrUserIdQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _context.Tasks
           .FromSqlRaw("EXEC GetTasksByStatus @p0, @p1", request.status, string.IsNullOrEmpty(request.userId) ? DBNull.Value : request.userId)
           .ToListAsync();

        return _mapper.Map<List<TaskDto>>(tasks);
    }
}
