using Application.Common.DTOs;

namespace Application.Accounts.GetAccounts;

public class GetAccountsResponse
{
    public int count { get; set; }
    public List<AccountDTO> Account { get; set; }
}