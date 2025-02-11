using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Errors;

namespace SampleProject.Application.Users.Commands.DeleteUser;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.userId);

        user!.isActive = false;

        var identityResult = await _userManager.UpdateAsync(user);

        if (!identityResult.Succeeded)
        {
            return Errors.User.UpdateFailed;
        }

        return true;

    }
}
