import React, { createContext, useContext, useState } from 'react';

// Создаем контекст для пользователя
const UserContext = createContext();

// Провайдер для пользователя
export const UserProvider = ({ children }) => {
    const [user, setUser ] = useState(() => {
        // Проверяем, есть ли пользователь в локальном хранилище
        const savedUser  = localStorage.getItem('user');
        return savedUser  ? JSON.parse(savedUser ) : null;
    });

    const login = (userData) => {
        setUser(userData); // Устанавливаем пользователя
    };

    const logout = () => {
        setUser(null); // Очищаем информацию о пользователе
        localStorage.removeItem('user'); // Сохраняем пользователя в локальном хранилище
    };

    const updateUser  = (updatedData) => {
        setUser((prevUser ) => ({ ...prevUser , ...updatedData })); // Обновляем информацию о пользователе
    };

    return (
        <UserContext.Provider value={{ user, login, logout, updateUser  }}>
            {children}
        </UserContext.Provider>
);
};

// Хук для использования контекста пользователя
export const useUser  = () => {
    return useContext(UserContext);
};