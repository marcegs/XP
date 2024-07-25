namespace Application.Common.DTOs;

public class AccountDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserDTO User { get; set; }
}