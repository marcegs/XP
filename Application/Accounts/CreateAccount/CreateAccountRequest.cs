using FluentResults;
using MediatR;

namespace Application.Accounts.CreateAccount;

public class CreateAccountRequest : IRequest<Result<CreateAccountResponse>>
{
    public string Name { get; set; }
    public int UserId { get; set; }
}


