using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Users.CreateUser;

public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
{
    private readonly IXpDbContext _context;
    private readonly IMapper _mapper;


    public CreateUserRequestHandler(IXpDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Birthdate = request.Birthdate,
            Username = request.Username
        };
        
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse { User = _mapper.Map<UserDTO>(user) };
    }
}
