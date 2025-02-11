using ErrorOr;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Tasks.Common;
namespace SampleProject.Application.Tasks.Queries.GetTasksDueToday;
public class GetTasksDueTodayQueryHandler
    : IRequestHandler<GetTasksDueTodayQuery, ErrorOr<List<TaskDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTasksDueTodayQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ErrorOr<List<TaskDto>>> Handle(GetTasksDueTodayQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _context.Tasks
           .FromSqlRaw("EXEC [GetTasksDueToday]") 
           .ToListAsync();

        return _mapper.Map<List<TaskDto>>(tasks);
    }
}
