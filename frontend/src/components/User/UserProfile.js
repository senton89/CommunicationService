import React, { useEffect, useState } from 'react';
import UserService from '../../services/UserService';
import { useParams } from 'react-router-dom';

const UserProfile = () => {
    const { id } = useParams();
    const [user, setUser ] = useState(null);

    useEffect(() => {
        UserService.getUserById(id).then(data => setUser (data));
    }, [id]);

    if (!user) return <div>Loading...</div>;

    return (
        <div>
            <h1>{user.name}</h1>
            <p>Email: {user.email}</p>
            <p>Bio: {user.bio}</p>
        </div>
    );
};

export default UserProfile;