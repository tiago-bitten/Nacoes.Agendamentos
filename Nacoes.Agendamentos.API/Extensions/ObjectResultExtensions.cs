using Microsoft.AspNetCore.Mvc;

namespace Nacoes.Agendamentos.API.Extensions;

public static class ObjectResultExtensions
{
    public static ObjectResult ComStatusCode(this ObjectResult result, int statusCode)
    {
        if (result is ObjectResult objectResult)
        {
            objectResult.StatusCode = statusCode;
        }

        return result;
    }
}
