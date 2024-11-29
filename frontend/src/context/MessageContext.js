import React, { createContext, useContext, useState } from 'react';

// Создаем контекст для сообщений
const MessageContext = createContext();

// Провайдер для сообщений
export const MessageProvider = ({ children }) => {
    const [messages, setMessages] = useState([]);

    const addMessage = (message) => {
        setMessages((prevMessages) => [...prevMessages, message]);
    };

    const removeMessage = (messageId) => {
        setMessages((prevMessages) => prevMessages.filter(message => message.id !== messageId));
    };

    const updateMessage = (updatedMessage) => {
        setMessages((prevMessages) =>
            prevMessages.map(message =>
                message.id === updatedMessage.id ? updatedMessage : message
            )
        );
    };

    return (
        <MessageContext.Provider value={{ messages, addMessage, removeMessage, updateMessage }}>
            {children}
        </MessageContext.Provider>
    );
};

// Хук для использования сообщений
export const useMessages = () => {
    return useContext(MessageContext);
};