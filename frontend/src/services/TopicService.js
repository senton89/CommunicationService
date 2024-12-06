import axios from 'axios';

const API_URL = 'http://localhost:5005/api/topics';

const TopicService = {
    // Получить все топики
    getTopics: async () => {
        const response = await axios.get(API_URL);
        return response.data;
    },

    // Создать новый топик
    createTopic: async (topic) => {
        const response = await axios.post(API_URL, topic);
        return response.data;
    },

    // Получить топик по ID
    getTopicById: async (id) => {
        const response = await axios.get(`${API_URL}/${id}`);
        return response.data;
    },

    // Обновить топик по ID
    updateTopic: async (id, topic) => {
        const response = await axios.put(`${API_URL}/${id}`, topic);
        return response.data;
    },

    // Удалить топик по ID
    deleteTopic: async (id) => {
        await axios.delete(`${API_URL}/${id}`);
    },
    // Получить топик по ID
    getTopicByTitle: async (title) => {
        const response = await axios.get(`${API_URL}/t=${title}`);
        return response.data;
    },
};

export default TopicService;