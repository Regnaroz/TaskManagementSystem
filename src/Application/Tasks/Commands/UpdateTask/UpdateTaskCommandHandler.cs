using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Common.Mappings;
using SampleProject.Application.Tasks.Commands.AddTask;
using SampleProject.Application.Tasks.Common;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Tasks.Commands.UpdateTask;
public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ErrorOr<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ErrorOr<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync(request.taskDto.Id);

        task!.Title = request.taskDto.Title;
        task!.Description = request.taskDto.Description;
        task!.Status = request.taskDto.Status;

        if(request.isAdmin)
        {
            task!.DueDate = request.taskDto.DueDate;
            task!.Priority= request.taskDto.Priority;
            task!.UserId = request.taskDto.UserId;
        }

        await _context.SaveChangesAsync(default);
        return true;
    }
}
