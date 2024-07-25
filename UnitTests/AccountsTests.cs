using Application.Accounts;

using Application.Accounts.DeleteAccount;
using Application.Accounts.GetAccountTrades;
using Application.Common.Interfaces;
using Application.Trades;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;

namespace UnitTests;

public class AccountsTests
{
    private readonly Mock<IXpDbContext> _context;

    private IMemoryCache GetCache()
    {
        return new MemoryCache(new MemoryCacheOptions());
    }
    public AccountsTests()
    {
        var trades = new List<Trade>
        {
            new Trade
            {
                Id = 1,
                AccountId = 1,
                Deleted = false,
                TradeAmmount = 50,
                TradeType = TradeType.Buy,
            },
            new Trade
            {
                Id = 4,
                AccountId = 1,
                Deleted = true,
                TradeAmmount = 50,
                TradeType = TradeType.Buy,
            },
            new Trade
            {
                Id = 1,
                AccountId = 1,
                Deleted = false,
                TradeAmmount = 30,
                TradeType = TradeType.Buy,
            },
            new Trade
            {
                Id = 1,
                AccountId = 1,
                Deleted = false,
                TradeAmmount = 66,
                TradeType = TradeType.Sell,
            },
            new Trade
            {
                Id = 6,
                AccountId = 2,
                Deleted = false,
                TradeAmmount = 66,
                TradeType = TradeType.Sell,
            }
        };
        var accounts = new List<Account>
        {
            new Account
            {
                Id = 1, Name = "account 1",
                Trades = trades
            },
            new Account { Id = 2, Name = "account 2", }
        };
        _context = new Mock<IXpDbContext>();
        _context.Setup(x => x.Accounts).ReturnsDbSet(accounts);
        _context.Setup(x => x.Trades).ReturnsDbSet(trades);
    }

    [Fact]
    public async Task DeleteAccountHandler_IdDoesNotExist_ShouldFail()
    {
        var logger = new Mock<ILogger<DeleteAccountRequestHandler>>();
        var request = new DeleteAccountRequest { Id = 3 };
        var handler = new DeleteAccountRequestHandler(_context.Object, logger.Object);

        var result = await handler.Handle(request, new System.Threading.CancellationToken());

        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.Equal($"Account with id {request.Id} not found", result.Errors[0].Message);
    }

    [Fact]
    public async Task GetAccountTrades_GetAccountId_ShouldReturnCorrectAmount()
    {
        var logger = new Mock<ILogger<GetAccountTradesRequestHandler>>();
        var mapper = new Mock<IMapper>();

        var request = new GetAccountTradesRequest() { Id = 1 };
        var handler = new GetAccountTradesRequestHandler(_context.Object, mapper.Object, logger.Object, GetCache());

        var result = await handler.Handle(request, new System.Threading.CancellationToken());

        Assert.True(result.IsSuccess);
        Assert.Equal(-14, result.Value.sum);
        Assert.Equal(3, result.Value.count);
    }
}