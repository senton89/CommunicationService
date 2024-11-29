import React, { useState } from 'react';

const MessageForm = ({ onSubmit }) => {
    const [message, setMessage] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        if (message.trim()) {
            // Создаем новый объект сообщения
            const newMessage = {
                content: message,
                timestamp: new Date().toISOString(),
            };

            // Вызываем функцию onSubmit для отправки сообщения
            onSubmit(newMessage);

            // Очищаем поле ввода
            setMessage('');
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