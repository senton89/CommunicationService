import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import UserService from '../../services/UserService';

const UserList = () => {
    const [users, setUsers] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const data = await UserService.getUsers();
                setUsers(data);
            } catch (err) {
                setError('Не удалось загрузить пользователей');
            }
        };

        fetchUsers();
    }, []);


    if (error) {
        return <p>{error}</p>;
    }

    return ( users );
};

export default UserList;