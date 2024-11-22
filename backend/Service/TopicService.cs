namespace ProfessionalCommunicationService;

public class TopicService
{

    private readonly TopicRepository _topicRepository;
    
    public TopicService(TopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }
    
    public async Task<List<Topic>> GetAllTopicsAsync()
    {
        return await _topicRepository.GetAllTopicsAsync();
    }
    
    public async Task<Topic> GetTopicByIdAsync(int id)
    {
        return await _topicRepository.GetTopicByIdAsync(id);
    }

    public async Task CreateTopicAsync(Topic topic)
    {
        // Здесь можно добавить логику для проверки валидности темы
        await _topicRepository.AddTopicAsync(topic);
    }

    public async Task UpdateTopicAsync(Topic topic)
    {
        await _topicRepository.UpdateTopicAsync(topic);
    }
    
    public async Task DeleteTopicAsync(int id)
    {
        await _topicRepository.DeleteTopicAsync(id);
    }
}