using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Application.Users.Commands.UpdateUser;
public record UpdateUserDto(string userId, string Email, string FirstName, string LastName);
