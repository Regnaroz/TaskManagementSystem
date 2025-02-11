using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace SampleProject.Web.Infrastructure;

[ApiController]
public class ApiController : ControllerBase
{
    protected ActionResult Problem(List<Error> errors)
    {
        //idk
        if (errors.Count is 0)
        {
            return Problem();
        }

        //if all errors are validation errors map them to dictionary and return theem
        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }
        //return first error
        return errors.Count == 1 ?  Problem(errors[0]) : ValidationProblem(errors);
    }

    private ActionResult ValidationProblem(List<Error> errors)
    {
        var errorDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            errorDictionary.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(errorDictionary);
    }

    private ActionResult Problem(Error firstError)
    {
        var StatusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var message = firstError.Description;
        return Problem(statusCode: StatusCode, title: message);
    }
}
