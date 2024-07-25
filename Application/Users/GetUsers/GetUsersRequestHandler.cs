using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.GetUsers;

public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, Result<GetUsersResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersRequestHandler(IXpDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Result<GetUsersResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _context.Users.AsNoTracking()
            .Include(user => user.Accounts)
            .ToListAsync(cancellationToken);

        return Result.Ok(new GetUsersResponse { users = _mapper.Map<List<UserDTO>>(users), count = users.Count });
    }
}
