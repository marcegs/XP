using FluentResults;

namespace Application.Common.Errors;

public class BadRequestError : Error
{
    public BadRequestError(string message) : base(message)
    {
    }
}