import axios from 'axios';

const API_URL = 'http://localhost:5005/api/users';

const UserService = {
    getUserById: async (id) => {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
},

    login: async (username, password) => {
            const response = await axios.post(`${API_URL}/signin`, { username, password });
            return response.data; // Предполагается, что API возвращает user
},

    getUsers: async () => {
    const response = await axios.get(API_URL);
    return response.data;
},
    createUser: async (user) => {
        console.log(user);
    const response = await axios.post(`${API_URL}/signup`, user);
    return response.data;
},
    updateUser: async (id, user) => {
        if(user.password === '') user.password = JSON.parse(localStorage.getItem('user')).password;
    const response = await axios.put(`${API_URL}/${id}`, user);
    return response.data;
},
    deleteUser: async (id) => {
    await axios.delete(`${API_URL}/${id}`);
},
};

export default UserService;