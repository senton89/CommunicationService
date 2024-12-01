import React, { useState } from 'react';
import MessageService from '../../services/MessageService';

const MessageForm = ({ senderId, receiverId, onMessageSent }) => {
    const [message, setMessage] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (message.trim()) {
            // Создаем новый объект сообщения
            const newMessage = {
                sender_id: senderId,
                receiver_id: receiverId,
                content: message,
                sent_at: new Date().toISOString(), // Используем ISO формат для даты
            };

            try {
                // Отправляем сообщение на сервер
                await MessageService.sendMessage(newMessage);

                // Вызываем функцию onMessageSent для обновления списка сообщений
                if (onMessageSent) {
                    onMessageSent(newMessage);
                }

                // Очищаем поле ввода
                setMessage('');
            } catch (error) {
                console.error("Error sending message:", error);
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <textarea
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    placeholder="Введите ваше сообщение..."
                    required
                />
            </div>
            <button type="submit">Отправить</button>
        </form>
    );
};

export default MessageForm;