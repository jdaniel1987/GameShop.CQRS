using CSharpFunctionalExtensions;

namespace GamesShop.API.Queries.Modules;

public static class ResultChecker
{
    public static Microsoft.AspNetCore.Http.IResult CheckResult<T>(IResult<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }
        else
        {
            return Results.BadRequest(result.Error);
        }
    }
}
