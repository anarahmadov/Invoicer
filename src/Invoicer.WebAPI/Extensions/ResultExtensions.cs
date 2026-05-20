using Invoicer.Application.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Invoicer.API.Controllers;

public static class ResultExtensions
{
    public static IActionResult ToResponse(this Result result, ControllerBase controller, string successMessage = "Success")
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        return result.IsSuccess
            ? controller.Ok(new { Message = successMessage })
            : controller.BadRequest(new { Error = result.Error });
    }

    public static IActionResult ToResponse<T>(this Result<T> result, ControllerBase controller)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        return result.IsSuccess
            ? controller.Ok(result.Value)
            : controller.BadRequest(new { Error = result.Error });
    }
}
