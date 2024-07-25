using FluentResults;
using MediatR;

namespace Application.Users.CreateUser;

public class CreateUserRequest : IRequest<Result<CreateUserResponse>>
{
    public string Username { get; set; }
    public DateTime Birthdate { get; set; }
}

