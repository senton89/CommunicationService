// src/components/Auth/Register.js

import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import UserService from "../../services/UserService"; // Импортируйте UserService
import { useUser  } from '../../context/UserContext'; // Импортируйте useUser
import './Modal.css';
import Login from "./Login";

const Register = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { login } = useUser ();
    const [isRegister, setIsRegister] = useState(true);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (password !== confirmPassword) {
            setError('Пароли не совпадают');
            return;
        }

        try {
            const user = await UserService.createUser({username, email, password});
            login(user); // Обновите состояние userId
            localStorage.setItem('user', JSON.stringify(user));
            navigate('/main');
            window.location.reload()
        } catch (err) {
            console.log(err);
            setError('Ошибка регистрации. Попробуйте еще раз.');
        }
    };
    const handleLogin = () => {
        setIsRegister(false)
    }

    return (
        isRegister?
            (<div className="register-container">
            <div className="register-header">
                <h2>Регистрация</h2>
            </div>
            {error && <p className="error-message">{error}</p>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <input type="text"
                           value={username}
                           onChange={(e) => setUsername(e.target.value)}
                           placeholder="Имя пользователя" required />
                </div>
                <div className="form-group">
                    <input type="email"
                           value={email}
                           onChange={(e) => setEmail(e.target.value)}
                           placeholder="Email" required />
                </div>
                <div className="form-group">
                    <input type="password"
                           value={password}
                           onChange={(e) => setPassword(e.target.value)}
                           placeholder="Пароль" required />
                </div>
                <div className="form-group">
                    <input type="password"
                           value={confirmPassword}
                           onChange={(e) => setConfirmPassword(e.target.value)}
                           placeholder="Подтвердите пароль" required />
                </div>
                <div className="form-group">
                    <button type="submit">Зарегистрироваться</button>
                    <button type="button" onClick={handleLogin}>Войти</button>
                </div>
            </form>
        </div>):<Login/>
    );
};

export default Register;