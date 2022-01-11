using Ardalis.Result;

namespace Langueedu.Core;

public static class Result
{
    public static Result<T> Invalid<T>(string errorMessage, string identifier)
    {
        return Result<T>.Invalid(new List<ValidationError>
                {
                    new ValidationError
                    {
                        Identifier = identifier,
                         ErrorMessage = errorMessage
                    }
                });
    }
}