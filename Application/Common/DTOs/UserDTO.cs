using System.Text.Json.Serialization;

namespace Application.Common.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public DateTime Birthdate { get; set; }
    public List<AccountDTO> Accounts { get; set; }
}