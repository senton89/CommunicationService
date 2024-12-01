namespace CommunicationService.DTO;

public class UserDTO
{
    public string? username { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}