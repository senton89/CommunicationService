namespace ProfessionalCommunicationService;

public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password_hash { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public string[] salt { get; }
}