using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using SampleProject.Application.Authentication.Common;

namespace SampleProject.Application.Authentication.Login.Commands.LoginUser;
public record LoginUserCommand(string email , string password) : IRequest<ErrorOr<AuthenticationResult>>;
