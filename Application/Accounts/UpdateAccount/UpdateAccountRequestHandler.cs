using Application.Common.DTOs;
using Application.Common.Errors;
using Application.Common.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Accounts.UpdateAccount;

public class UpdateAccountRequestHandler : IRequestHandler<UpdateAccountRequest, Result<UpdateAccountResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAccountRequestHandler> _logger;

    public UpdateAccountRequestHandler(IXpDbContext context, IMapper mapper, ILogger<UpdateAccountRequestHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<UpdateAccountResponse>> Handle(UpdateAccountRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.Where(user => user.Id == request.UserId && !user.Deleted).FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User with id {id} not found", request.UserId);
            var error = new EntityNotFoundError($"User with id {request.UserId} not found");
            return Result.Fail(error);
        }

        var account = await _context.Accounts.Where(acc => acc.Id == request.Id && !acc.Deleted).FirstOrDefaultAsync(cancellationToken);
        if (account == null)
        {
            _logger.LogWarning("Account with id {id} not found", request.Id);
            var error = new EntityNotFoundError($"Account with id {request.Id} not found");
            return Result.Fail(error);
        }

        account.Name = request.Name;
        account.UserId = request.UserId;

        _context.Accounts.Update(account);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateAccountResponse { Account = _mapper.Map<AccountDTO>(account) };
    }
}
