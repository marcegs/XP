using FluentResults;
using MediatR;

namespace Application.Users.UpdateUser;

public class UpdateUserRequest : IRequest<Result<UpdateUserResponse>>
{
    public int Id { get; set; }
    public string Username { get; set; }
    public DateTime Birthdate { get; set; }
}
