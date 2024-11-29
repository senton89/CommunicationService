import axios from 'axios';

const API_URL = 'http://localhost:5005/api/posts';

const PostService = {
    getPosts: async () => {
        const response = await axios.get(API_URL);
        return response.data;
    },
    getPostById: async (id) => {
        const response = await axios.get(`${API_URL}/${id}`);
        return response.data;
    },
    createPost: async (post) => {
        const response = await axios.post(API_URL, post);
        return response.data;
    },
    updatePost: async (id, post) => {
        const response = await axios.put(`${API_URL}/${id}`, post);
        return response.data;
    },
    deletePost: async (id) => {
        await axios.delete(`${API_URL}/${id}`);
    },
};

export default PostService;
