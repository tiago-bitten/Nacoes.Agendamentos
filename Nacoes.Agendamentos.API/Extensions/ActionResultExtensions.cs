using Microsoft.AspNetCore.Mvc;

namespace Nacoes.Agendamentos.API.Extensions;

public static class ActionResultExtensions
{
    public static IActionResult ComStatusCode(this IActionResult result, int statusCode)
    {
        if (result is ObjectResult objectResult)
        {
            objectResult.StatusCode = statusCode;
        }

        return result;
    }

}
