using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
namespace SampleProject.Domain.Errors;
public static partial class Errors
{
    public static partial class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Authentication.InvalidCredentials",
            description: "Provided Credentials Aren't Valid");

        public static Error WeakPassword => Error.Validation(
           code: "Authentication.WeakPassword",
           description: "Provided password is weak! .");
        public static Error DuplicateEmail => Error.Validation(
           code: "Authentication.DuplicateEmail",
           description: "Email Duplication");
              public static Error InvalidEmail => Error.Validation(
           code: "Authentication.InvalidEmail",
           description: "Invalid Email Address");
        public static Error UserNotFound => Error.Validation(
        code: "Authentication.UserNotFound",
        description: "User was not found! .");
    }
}
