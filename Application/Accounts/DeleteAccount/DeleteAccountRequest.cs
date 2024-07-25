using FluentResults;
using MediatR;

namespace Application.Accounts.DeleteAccount;

public class DeleteAccountRequest : IRequest<Result<DeleteAccountResponse>>
{
    public int Id { get; set; }
}

