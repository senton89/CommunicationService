using CommunicationService;
using CommunicationService.DTO;

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
    public async Task<Topic> GetTopicByTitleAsync(string title)
    {
        return await _topicRepository.GetTopicByTitleAsync(title);
    }

    public async Task<ResponseStatus> CreateTopicAsync(TopicDTO topicDto)
    {
        // Здесь можно добавить логику для проверки валидности темы
        return await _topicRepository.AddTopicAsync(topicDto);
    }

    public async Task<ResponseStatus> UpdateTopicAsync(TopicDTO topicDto,int id)
    {
        return await _topicRepository.UpdateTopicAsync(topicDto,id);
    }
    
    public async Task<ResponseStatus> DeleteTopicAsync(int id)
    {
        return await _topicRepository.DeleteTopicAsync(id);
    }
}