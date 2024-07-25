using Application.Common.Errors;
using Application.Common.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Users.DeleteUser;

public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Result<DeleteUserResponse>>
{
    private readonly IXpDbContext _context;
    private readonly ILogger<DeleteUserRequestHandler> _logger;
    
    public DeleteUserRequestHandler(IXpDbContext context, ILogger<DeleteUserRequestHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<DeleteUserResponse>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Where(a => a.Deleted == false && a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User with id {request.Id} not found", request.Id);
            var error = new EntityNotFoundError($"User with id {request.Id} not found");
            return Result.Fail(error);
        }
        
        user.Deleted = true;
        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteUserResponse();
    }
}
