using Application.Common.DTOs;

namespace Application.Users.GetUsers;

public class GetUsersResponse
{
    public int count { get; set; }
    public List<UserDTO> users { get; set; }
}