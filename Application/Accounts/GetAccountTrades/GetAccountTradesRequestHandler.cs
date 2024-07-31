using Application.Common.DTOs;
using Application.Common.Errors;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Accounts.GetAccountTrades;

public class GetAccountTradesRequestHandler : IRequestHandler<GetAccountTradesRequest, Result<GetAccountTradesResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAccountTradesRequestHandler> _logger;
    private readonly IMemoryCache _memoryCache;

    public GetAccountTradesRequestHandler(IXpDbContext context, IMapper mapper, ILogger<GetAccountTradesRequestHandler> logger, IMemoryCache memoryCache)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task<Result<GetAccountTradesResponse>> Handle(GetAccountTradesRequest request, CancellationToken cancellationToken)
    {
        var account = _memoryCache.Get<Account>($"account_{request.Id}");
        if (account == null)
        {
            account = await _context.Accounts
                .AsNoTracking()
                .Where(acc => acc.Id == request.Id && !acc.Deleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (account != null) _memoryCache.Set<Account>($"account_{request.Id}", account, TimeSpan.FromSeconds(50));
        }

        if (account == null)
        {
            _logger.LogWarning("Account with id {id} not found", request.Id);
            var error = new EntityNotFoundError($"Account with id {request.Id} not found");
            return Result.Fail(error);
        }

        var trades = _memoryCache.Get<List<Trade>>($"tradesByAccountId_{request.Id}");
        if (trades == null)
        {
            trades = await _context.Trades.AsNoTracking()
                .Where(trade => trade.AccountId == request.Id && !trade.Deleted)
                .ToListAsync(cancellationToken);
            _memoryCache.Set<List<Trade>>($"tradesByAccountId_{request.Id}", trades, TimeSpan.FromSeconds(50));
        }

        var totalBuy = trades.Where(a => a.TradeType == TradeType.Buy).Sum(a => a.TradeAmmount);
        var totalSell = trades.Where(a => a.TradeType == TradeType.Sell).Sum(a => a.TradeAmmount);
        totalBuy *= -1;
        return new GetAccountTradesResponse
        {
            sum = totalBuy + totalSell,
            count = trades.Count,
            Trades = _mapper.Map<List<TradeDTO>>(trades)
        };
    }
}