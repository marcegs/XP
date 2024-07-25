using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.GetAccounts;

public class GetAccountsRequestHandler : IRequestHandler<GetAccountsRequest, Result<GetAccountsResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;

    public GetAccountsRequestHandler(IXpDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<GetAccountsResponse>> Handle(GetAccountsRequest request,
        CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.AsNoTracking()
            .Include(a => a.User)
            .ToListAsync(cancellationToken);

        return new GetAccountsResponse { Account = _mapper.Map<List<AccountDTO>>(account), count = account.Count };
    }
}