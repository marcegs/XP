using FluentResults;
using MediatR;

namespace Application.Users.GetUsers;

public class GetUsersRequest : IRequest<Result<GetUsersResponse>>
{
    public int Id { get; set; }
}