using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Errors;

namespace SampleProject.Application.Tasks.Commands.DeleteTask;
public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ErrorOr<bool>>
{
    private readonly IApplicationDbContext _context;
    public DeleteTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync(request.taskId);

        task!.IsDeleted = true;

        await _context.SaveChangesAsync(default);
        return true;

    }
}
