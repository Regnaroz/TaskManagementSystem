using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Application.Authentication.Register.Commands.RegisterUser;
public record RegisterUserDto(
      string firstName,
      string lastName,
      string password,
      string email
      );
