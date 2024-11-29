import React, { createContext, useContext, useState } from 'react';

// Создаем контекст для пользователя
const UserContext = createContext();

// Провайдер для пользователя
export const UserProvider = ({ children }) => {
    const [user, setUser ] = useState(null); // Состояние для хранения информации о пользователе

    const login = (userData) => {
        setUser (userData); // Устанавливаем пользователя
    };

    const logout = () => {
        setUser (null); // Очищаем информацию о пользователе
    };

    const updateUser  = (updatedData) => {
        setUser ((prevUser ) => ({ ...prevUser , ...updatedData })); // Обновляем информацию о пользователе
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