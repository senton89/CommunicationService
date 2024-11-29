import axios from 'axios';

const API_URL = 'http://localhost:5005/api/messages';

const MessageService = {
    getMessages: async () => {
        const response = await axios.get(API_URL);
        return response.data;
    },
    sendMessage: async (message) => {
        const response = await axios.post(API_URL, message);
        return response.data;
    },
    deleteMessage: async (id) => {
        await axios.delete(`${API_URL}/${id}`);
    },
};

export default MessageService;