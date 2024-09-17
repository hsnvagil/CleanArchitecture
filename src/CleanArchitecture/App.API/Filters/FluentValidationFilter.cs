using App.Application.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class FluentValidationFilter : IAsyncActionFilter {
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        if (!context.ModelState.IsValid) {
            var errors = (from x in context.ModelState.Values from error in x.Errors select error.ErrorMessage)
                .ToList();

            var resultModel = ServiceResult.Fail(errors);

            context.Result = new BadRequestObjectResult(resultModel);
            return;
        }

        await next();
    }
}