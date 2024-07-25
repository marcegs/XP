using FluentResults;
using MediatR;

namespace Application.Accounts.GetAccountTrades;

public class GetAccountTradesRequest : IRequest<Result<GetAccountTradesResponse>>
{
    public int Id { get; set; }
}