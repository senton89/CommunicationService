namespace ProfessionalCommunicationService;

public class Topic
{
    public int id { get; set; }
    public string title { get; set; }
    public int author_id { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}