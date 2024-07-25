using Application.Common.Errors;
using Application.Common.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Accounts.DeleteAccount;

public class DeleteAccountRequestHandler : IRequestHandler<DeleteAccountRequest, Result<DeleteAccountResponse>>
{
    private readonly IXpDbContext _context;
    private readonly ILogger<DeleteAccountRequestHandler> _logger;

    public DeleteAccountRequestHandler(IXpDbContext context, ILogger<DeleteAccountRequestHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<DeleteAccountResponse>> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.Where(acc => acc.Id == request.Id && !acc.Deleted).FirstOrDefaultAsync(cancellationToken);
        if (account == null)
        {
            _logger.LogWarning("Account with id {request.Id} not found", request.Id);
            var error = new EntityNotFoundError($"Account with id {request.Id} not found");
            return Result.Fail(error);
        }

        account.Deleted = true;

        _context.Accounts.Update(account);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteAccountResponse();
    }
}