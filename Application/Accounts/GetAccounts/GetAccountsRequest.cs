using FluentResults;
using MediatR;

namespace Application.Accounts.GetAccounts;

public class GetAccountsRequest : IRequest<Result<GetAccountsResponse>>
{
}
