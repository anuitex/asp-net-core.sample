using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.API.Filters
{
    public class ModelStateValidationActionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                string errorMessage = GetFirstModelStateError(context.ModelState);
                context.Result = new BadRequestObjectResult(errorMessage);
            }
        }

        private string GetFirstModelStateError(ModelStateDictionary modelState)
        {
            string errorMessage = modelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).FirstOrDefault();

            return errorMessage;
        }
    }
}
