using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using SampleProject.Application.Authentication.Common;

namespace SampleProject.Application.Users.Commands.UpdateUser;
public record UpdateUserCommand(UpdateUserDto updateUserDto) : IRequest<ErrorOr<bool>>;
