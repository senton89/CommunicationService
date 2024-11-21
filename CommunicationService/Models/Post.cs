namespace ProfessionalCommunicationService;

public class Post
{
    public int id { get; set; }
    public int topic_id { get; set; }
    public string content { get; set; }
    public int author_id { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}