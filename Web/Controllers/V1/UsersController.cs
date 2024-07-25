using Application.Trades;
using Application.Users;
using Application.Users.CreateUser;
using Application.Users.DeleteUser;
using Application.Users.GetUsers;
using Application.Users.UpdateUser;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.V1;

[Route("api/v1/[controller]")]
public class UsersController : BaseController<UsersController>
{
    public UsersController(ILogger<UsersController> logger) : base(logger)
    {
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserRequest request)
    {
        var result = await Mediator.Send(request);
        return HandleResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<DeleteUserResponse>> DeleteUser(int id)
    {
        var result = await Mediator.Send(new DeleteUserRequest { Id = id });
        return HandleResult(result);
    }

    [HttpPatch]
    public async Task<ActionResult<UpdateUserResponse>> UpdateUser(UpdateUserRequest request)
    {
        var result = await Mediator.Send(request);
        return HandleResult(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<GetUsersResponse>> GetUsers()
    {
        var result = await Mediator.Send(new GetUsersRequest());
        return HandleResult(result);
    }
}