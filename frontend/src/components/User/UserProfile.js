import React, { useEffect, useState } from 'react';
import './UserProfileStyle.css';
import UserService from '../../services/UserService';
import {useNavigate} from "react-router-dom";
import {UserProvider} from "../../context/UserContext"; // Импортируем UserService

const UserProfile = () => {
    const [userData, setUserData] = useState({ name: '', email: '', password: '' });
    const userId = JSON.parse(localStorage.getItem('user')).id;
    const navigate = useNavigate();

    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        setUserData({
            name: params.get('name') || '',
            email: params.get('email') || '',
            password: '' // Инициализируем пароль как пустую строку
        });
    }, []);

    const handleChange = (e) => {
        const { id, value } = e.target;
        setUserData((prevData) => ({
            ...prevData,
            [id]: value
        }));
    };

    const handleSaveClick = async () => {
        try {
            const updatedUser  = await UserService.updateUser(userId, userData);
            console.log('User updated successfully:', updatedUser );
            navigate(`/main`);
        } catch (error) {
            console.error('Error updating user:', error);
        }
    };

    const handleLogOut = () => {
        try {
            navigate(`/register`);
            localStorage.removeItem('user');
        }catch(Ex)
        {
            console.log(Ex);
        }
    };

    return (
        <div className="user-profile-container">
            <div className="user-profile-header">
                <img alt="Profile picture" src="https://placehold.co/100x100" />
                <h2>User Profile</h2>
            </div>
            <div className="user-profile-form-group">
                <label htmlFor="name">Name</label>
                <input type="text" id="name" value={userData.name} onChange={handleChange} placeholder="Enter your name" required />
            </div>
            <div className="user-profile-form-group">
                <label htmlFor="email">Email</label>
                <input type="email" id="email" value={userData.email} onChange={handleChange} placeholder="Enter your email" required />
            </div>
            <div className="user-profile-form-group">
                <label htmlFor="password">Password</label>
                <input type="password" id="password" value={userData.password} onChange={handleChange} placeholder="Enter password to change" />
            </div>
            <div className="user-profile-footer">
                <button type="button" onClick={handleSaveClick}>Save Profile</button>
            </div>
            <div className="user-profile-footer">
                <button type="button" onClick={handleLogOut}>Log out</button>
            </div>
        </div>
    );
};

export default UserProfile;