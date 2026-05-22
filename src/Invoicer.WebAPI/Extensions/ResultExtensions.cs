using Invoicer.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Invoicer.API.Controllers;

public static class ResultExtensions
{
    public static IActionResult ToResponse(this Result result, ControllerBase controller)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        return result.IsSuccess
            ? controller.Ok(result)
            : controller.BadRequest(result);
    }

    public static IActionResult ToResponse<T>(this Result<T> result, ControllerBase controller)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        return result.IsSuccess
            ? controller.Ok(result)
            : controller.BadRequest(result);
    }
}
