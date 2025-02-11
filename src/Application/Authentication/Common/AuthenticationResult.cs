using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Authentication.Common;
public record AuthenticationResult(
        UserDto user,
        string token
    );
