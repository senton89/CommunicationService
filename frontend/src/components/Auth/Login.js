// src/components/Auth/Login.js

import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import AuthService from '../../services/AuthService';
import { useUser  } from '../../context/UserContext';
import UserService from "../../services/UserService"; // Импортируйте useUser
import './Modal.css';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { login } = useUser();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const user = await UserService.login(username, password);
            login(user); // Обновите состояние userId
            localStorage.setItem('user', JSON.stringify(user));
            navigate('/main');
            window.location.reload()
        } catch (err) {
            console.log(err);
            setError('Неверные учетные данные');
        }
    };

    return (
        <div className="login-container">
            <div className="login-header">
                <h2>Login</h2>
            </div>
            {error && <p>{error}</p>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <input type="text"
                           value={username}
                           onChange={(e)=> setUsername(e.target.value)}
                           placeholder="Username" required/>
                </div>
                <div className="form-group">
                    <input type="password" value={password}
                           onChange=
                               {(e)=> setPassword(e.target.value)}
                           placeholder="Password" required/>
                </div>
                <div className="form-group">
                    <button type="submit">Login</button>
                </div>
            </form>
        </div>
    );
};

export default Login;