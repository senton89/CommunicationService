import axios from 'axios';

const API_URL = 'http://localhost:5005/api/messages';

const MessageService = {
    // Получение сообщений между отправителем и получателем
    getMessages: async (senderId, receiverId) => {
        const response = await axios.get(`${API_URL}/${senderId}/${receiverId}`);
        return response.data;
    },

    getMessagesByUserId: async (userId) => {
    const response = await fetch(`${API_URL}/${userId}`);
    if (!response.ok) {
        throw new Error('Failed to fetch messages for user');
    }
    return await response.json();
},

    // Отправка сообщения
    sendMessage: async (message) => {
        const response = await axios.post(API_URL, message);
        return response.data;
    },

    // Обновление сообщения
    updateMessage: async (id, updatedMessage) => {
        const response = await axios.put(`${API_URL}/${id}`, updatedMessage);
        return response.data;
    },

    // Удаление сообщения
    deleteMessage: async (id) => {
        await axios.delete(`${API_URL}/${id}`);
    },
};

export default MessageService;