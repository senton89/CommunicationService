import React, { createContext, useContext, useState, useEffect } from 'react';
import MessageService from '../services/MessageService';

// Создаем контекст для сообщений
const MessageContext = createContext();

// Провайдер для сообщений
export const MessageProvider = ({ userId, children }) => {
    const [messages, setMessages] = useState([]);

    // Функция для загрузки последних сообщений пользователя
    const fetchLatestMessages = async () => {
        try {
            const data = await MessageService.getMessagesByUserId(userId);
            setMessages(data);
        } catch (error) {
            console.error("Error fetching latest messages:", error);
        }
    };

    // Функция для загрузки сообщений между отправителем и получателем
    const fetchMessagesBetween = async (senderId, receiverId) => {
        try {
            const data = await MessageService.getMessages(senderId, receiverId);
            return data;
        } catch (error) {
            console.error("Error fetching messages between users:", error);
            return [];
        }
    };

    // Эффект для загрузки последних сообщений при монтировании компонента
    useEffect(() => {
        fetchLatestMessages();
    }, [userId]); // Обновление при изменении userId

    const addMessage = async (message) => {
        try {
            const response = await MessageService.sendMessage(message);
            setMessages((prevMessages) => [...prevMessages, response]);
        } catch (error) {
            console.error("Error sending message:", error);
        }
    };

    const removeMessage = async (messageId) => {
        try {
            await MessageService.deleteMessage(messageId);
            setMessages((prevMessages) => prevMessages.filter(message => message.id !== messageId));
        } catch (error) {
            console.error("Error deleting message:", error);
        }
    };

    const updateMessage = async (updatedMessage) => {
        try {
            await MessageService.updateMessage(updatedMessage.id, updatedMessage);
            setMessages((prevMessages) =>
                prevMessages.map(message =>
                    message.id === updatedMessage.id ? updatedMessage : message
                )
            );
        } catch (error) {
            console.error("Error updating message:", error);
        }
    };

    return (
        <MessageContext.Provider value={{ messages, addMessage, removeMessage, updateMessage, fetchMessagesBetween }}>
            {children}
        </MessageContext.Provider>
    );
};

// Хук для использования сообщений
export const useMessages = () => {
    return useContext(MessageContext);
};