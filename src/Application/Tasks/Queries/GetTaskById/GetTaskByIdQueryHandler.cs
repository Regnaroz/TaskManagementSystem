using ErrorOr;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Tasks.Common;
namespace SampleProject.Application.Tasks.Queries.GetTaskById;
public class GetTaskByIdQueryHandler
    : IRequestHandler<GetTaskByIdQuery, ErrorOr<TaskDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ErrorOr<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync(request.Id);
        return _mapper.Map<TaskDto>(task); 
    }
}
