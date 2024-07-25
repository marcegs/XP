using FluentResults;

namespace Application.Common.Errors;

public class EntityNotFoundError : Error
{
    public EntityNotFoundError(string message) : base(message)
    {
    }
}