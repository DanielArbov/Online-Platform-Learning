using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Talent;

public class CatchAllFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        string message =context.Exception.Message;
        InternalServerError error =new InternalServerError(message);
        JsonResult result = new JsonResult(error);
        result.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = result;
        context.ExceptionHandled=true;
    }


   
}
