using Application.Accounts;
using Application.Accounts.CreateAccount;
using Application.Accounts.DeleteAccount;
using Application.Accounts.GetAccounts;
using Application.Accounts.GetAccountTrades;
using Application.Accounts.UpdateAccount;
using Application.Trades;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers.V1;

[Route("api/v1/[controller]")]
public class AccountsController : BaseController<AccountsController>
{
    public AccountsController(ILogger<AccountsController> logger) : base(logger)
    {
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<DeleteAccountResponse>> DeleteAccount(int id)
    {
        var request = new DeleteAccountRequest { Id = id };
        var result = await Mediator.Send(request);
        
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<CreateAccountResponse>> CreateAccount(CreateAccountRequest request)
    {
        var result = await Mediator.Send(request);
        return HandleResult(result);
    }

    [HttpPatch]
    public async Task<ActionResult<UpdateAccountResponse>> UpdateAccount(UpdateAccountRequest request)
    {
        var result = await Mediator.Send(request);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<ActionResult<GetAccountsResponse>> GetAccounts()
    {
        var result = await Mediator.Send(new GetAccountsRequest());
        return HandleResult(result);
    }

    [HttpGet("{id:int}/trades")]
    public async Task<ActionResult<GetAccountTradesResponse>> GetAccountTrades(int id)
    {
        var result = await Mediator.Send(new GetAccountTradesRequest { Id = id });
        return HandleResult(result);
    }
}