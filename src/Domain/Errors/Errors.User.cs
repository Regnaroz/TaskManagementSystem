using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace SampleProject.Domain.Errors;
public static partial class Errors
{
    public static partial class User
    {
        public static Error UpdateFailed => Error.Validation(
               code: "User.UpdateUserFailed",
               description: "Update User Failed");
    }
}
