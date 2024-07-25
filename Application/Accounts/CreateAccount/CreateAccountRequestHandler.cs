using Application.Common.DTOs;
using Application.Common.Errors;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Accounts.CreateAccount;

public class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, Result<CreateAccountResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAccountRequestHandler> _logger;

    public CreateAccountRequestHandler(IXpDbContext context, IMapper mapper, ILogger<CreateAccountRequestHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<CreateAccountResponse>> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateAccountValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid request: {errors}", validationResult.Errors);
            var errors = validationResult.Errors.Select(failure => new BadRequestError(failure.ErrorMessage)).ToList();
            return Result.Fail(errors);
        }

        var user = await _context.Users.Where(user => user.Id == request.UserId && !user.Deleted).FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User with id {request.UserId} not found", request.UserId);
            var error = new EntityNotFoundError($"User with id {request.UserId} not found");
            return Result.Fail(error);
        }

        var account = new Account
        {
            Name = request.Name,
            UserId = request.UserId
        };
        await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateAccountResponse { Account = _mapper.Map<AccountDTO>(account) };
    }
}
