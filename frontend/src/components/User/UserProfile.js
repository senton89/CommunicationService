import React, { useEffect, useState } from 'react';
import UserService from '../../services/UserService';
import { useParams } from 'react-router-dom';

const UserProfile = () => {
    const { id } = useParams();
    const [user, setUser ] = useState(null);
    const [isEditing, setIsEditing] = useState(false);
    const [updatedUser , setUpdatedUser ] = useState({});
    const [error, setError] = useState(null); // Состояние для хранения ошибок

    useEffect(() => {
        const fetchUser  = async () => {
            try {
                const data = await UserService.getUserById(id);
                setUser (data);
                setUpdatedUser (data);
            } catch (err) {
                setError('Failed to load user data'); // Устанавливаем ошибку
            }
        };

        fetchUser ();
    }, [id]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUpdatedUser (prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSave = async () => {
        try {
            await UserService.updateUser (id, updatedUser );
            setUser (updatedUser );
            localStorage.setItem('user', JSON.stringify(updatedUser ));
            setIsEditing(false);
        } catch (err) {
            setError('Failed to update user data'); // Устанавливаем ошибку
        }
    };

    if (error) return <div>Error: {error}</div>; // Отображаем ошибку
    if (!user) return <div>Loading...</div>;

    return (
        <div>
            <h1>{isEditing ? 'Edit User Profile' : user.name}</h1>
            {isEditing ? (
                <form onSubmit={(e) => { e.preventDefault(); handleSave(); }}>
                    <div>
                        <label>
                            Name:
                            <input
                                type="text"
                                name="name"
                                value={updatedUser .name}
                                onChange={handleChange}
                                required
                            />
                        </label>
                    </div>
                    <div>
                        <label>
                            Email:
                            <input
                                type="email"
                                name="email"
                                value={updatedUser .email}
                                onChange={handleChange}
                                required
                            />
                        </label>
                    </div>
                    <div>
                        <label>
                            Bio:
                            <textarea
                                name="bio"
                                // value={updatedUser .bio}
                                onChange={handleChange}
                            />
                        </label>
                    </div>
                    <button type="submit">Save Changes</button>
                    <button type="button" onClick={() => setIsEditing(false)}>Cancel</button>
                </form>
            ) : (
                <div>
                    <p>Email: {user.email}</p>
                    {/*<p>Bio: {user.bio}</p>*/}
                    <button onClick={() => setIsEditing(true)}>Edit Profile</button>
                </div>
            )}
        </div>
    );
};

export default UserProfile;