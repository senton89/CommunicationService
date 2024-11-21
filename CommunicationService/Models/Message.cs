namespace ProfessionalCommunicationService;

public class Message
{
    public int id { get; set; }
    public int sender_id { get; set; }
    public int receiver_id { get; set; }
    public string content { get; set; }
    public DateTime sent_at { get; set; }
}