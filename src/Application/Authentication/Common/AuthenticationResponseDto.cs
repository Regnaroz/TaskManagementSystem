using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Application.Authentication.Common;
public record AuthenticationResponseDto(
       Guid Id,
       string firstName,
       string lastName,
       string email,
       string token);
