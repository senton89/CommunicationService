namespace ProfessionalCommunicationService;

public class Comment
{
    public int id { get; set; }
    public int post_id { get; set; }
    public string content { get; set; }
    public int author_id { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}