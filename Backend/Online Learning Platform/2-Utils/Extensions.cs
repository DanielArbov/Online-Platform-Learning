using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Talent;

public static class Extensions
{
    public static string GetFirstError(this ModelStateDictionary modelState) // use this extension if you only want to return the first error
    {
        return modelState.Values.Where(v => v.Errors.Any()).First().Errors.First().ErrorMessage;
    }

    public static string GetAllErrors(this ModelStateDictionary modelState)
    // use this extension if you only
    // want to return all of the errors
    {
        string errors = "";
        // get the error by running on all failed properties( e.g: ProductName):
        foreach (KeyValuePair<string, ModelStateEntry> item in modelState)
        {
            // Running on all failed attributes of the item properties (e.g: Required, MaxLength, etc...):
            foreach (ModelError err in item.Value.Errors)
            {
                errors += err.ErrorMessage + "; ";
            }
        }
        return errors;
    }
}