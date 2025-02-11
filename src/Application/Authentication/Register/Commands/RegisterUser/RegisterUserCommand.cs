using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using SampleProject.Application.Authentication.Common;

namespace SampleProject.Application.Authentication.Register.Commands.RegisterUser;
public record RegisterUserCommand(
        string firstName,
        string lastName,
        string password,
        string email)
    : IRequest<ErrorOr<string>>;
