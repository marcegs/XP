using FluentResults;
using MediatR;

namespace Application.Accounts.UpdateAccount;

public class UpdateAccountRequest : IRequest<Result<UpdateAccountResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
}
