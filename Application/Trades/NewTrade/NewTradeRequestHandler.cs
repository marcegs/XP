using Application.Common.Errors;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Trades.NewTrade;

public class NewTradeRequestHandler : IRequestHandler<NewTradeRequest, Result<NewTradeResponse>>
{
    private readonly IXpDbContext _context;

    public NewTradeRequestHandler(IXpDbContext context, IMemoryCache cache)
    {
        _context = context;
    }

    public async Task<Result<NewTradeResponse>> Handle(NewTradeRequest request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .Where(acc => acc.Id == request.AccountId && !acc.Deleted)
            .FirstOrDefaultAsync(cancellationToken);
        if (account == null)
        {
            var error = new EntityNotFoundError($"Account with id {request.AccountId} not found");
            return Result.Fail(error);
        }
        
        var product = await _context.Products
            .Where(product => product.Id == request.ProductId && !product.Deleted && DateTime.UtcNow < product.ExpirationDate)
            .FirstOrDefaultAsync(cancellationToken);
        if (product == null)
        {
            var error = new EntityNotFoundError($"Product with id {request.ProductId} not found or expired.");
            return Result.Fail(error);
        }

        var trade = new Trade
        {
            AccountId = account.Id,
            Account = account,
            TradeType = request.TradeType,
            TradeAmmount = request.TradeAmmount,
            ProductId = request.ProductId,
            Product = product
        };

        await _context.Trades.AddAsync(trade, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new NewTradeResponse();
    }
}
