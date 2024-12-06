import axios from 'axios';

const API_URL = 'http://localhost:5005/api/posts';

const CommentService = {
    // Получить все комментарии к посту
    getComments: async (postId) => {
        const response = await axios.get(`${API_URL}/${postId}/comments`);
        return response.data;
    },

    // Создать новый комментарий
    createComment: async (postId,comment) => {
        const response = await axios.post(`${API_URL}/${postId}/comments`, comment);
        return response.data;
    },
};

export default CommentService;