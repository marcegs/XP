using FluentResults;
using MediatR;

namespace Application.Users.DeleteUser;
public class DeleteUserRequest : IRequest<Result<DeleteUserResponse>>
{
    public int Id { get; set; }
}
