using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Common.Mappings;
using SampleProject.Application.Tasks.Common;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Tasks.Commands.AddTask;
public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, ErrorOr<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public AddTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ErrorOr<int>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new UserTask()
        {
            Title = request.taskDto.Title,
            Description = request.taskDto.Description,
            Status = request.taskDto.Status,
            Priority = request.taskDto.Priority,
            DueDate = request.taskDto.DueDate,
            UserId = request.taskDto.UserId
        };

        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync(default);
        
        return task.Id;
    }
}
