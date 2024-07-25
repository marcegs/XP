using Application.Common.DTOs;
using Application.Common.Errors;
using Application.Common.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Users.UpdateUser;

public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, Result<UpdateUserResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateUserRequestHandler> _logger;

    public UpdateUserRequestHandler(IXpDbContext context, IMapper mapper, ILogger<UpdateUserRequestHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Where(a => a.Deleted == false && a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User with id {request.Id} not found", request.Id);
            var error = new EntityNotFoundError($"User with id {request.Id} not found");
            return Result.Fail(error);
        }

        user.Birthdate = request.Birthdate;
        user.Username = request.Username;

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateUserResponse { User = _mapper.Map<UserDTO>(user) };
    }
}
