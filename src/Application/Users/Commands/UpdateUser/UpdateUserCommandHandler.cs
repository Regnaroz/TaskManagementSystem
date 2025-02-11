using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Errors;

namespace SampleProject.Application.Users.Commands.UpdateUser;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.updateUserDto.userId);

        user!.FirstName = request.updateUserDto?.FirstName;
        user.Email = request.updateUserDto?.Email;
        user.LastName = request.updateUserDto?.LastName;

        var identityResult = await _userManager.UpdateAsync(user);

        if (!identityResult.Succeeded)
        {
            return Errors.User.UpdateFailed;
        }

        return true;
    }
}
