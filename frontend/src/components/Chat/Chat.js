import React from 'react';
import { useParams } from 'react-router-dom';
import MessageList from "../Messages/MessageList"; // Импортируйте список сообщений
import UserList from "../User/UserList";
import ChatList from "./ChatList"; // Импортируйте список пользователей

const Chat = (contact_id) => {
    const { userId } = useParams(); // Получаем userId из параметров маршрута
    const messages = ChatList(userId,contact_id); // Получаем сообщения для выбранного пользователя
    const users = UserList(); // Получаем список пользователей

    // Создаем объект для быстрого поиска username по sender_id
    const userMap = users.reduce((acc, user) => {
        acc[user.id] = user.username;
        return acc;
    }, {});

    return (
        <div className="container mx-auto my-12 bg-white rounded-2xl shadow-lg p-4">
            <h2 className="font-semibold mb-5">Chat with {userMap[userId]}</h2>
            <div className="message-list">
                {messages.map((message, index) => (
                    <div key={index} className={`message-item ${message.sender_id === userId ? 'sent' : 'received'} py-2`}>
                        <p className="font-semibold">{userMap[message.sender_id]}</p>
                        <p className="text-xs text-gray-500">{message.content}</p>
                    </div>
                ))}
            </div>
            {/* Здесь можно добавить форму для отправки нового сообщения */}
            <div className="message-input mt-5">
                <input type="text" placeholder="Type a message..." className="border-b border-gray-200 py-1 px-2 w-full" />
                <button className="bg-gray-800 text-white rounded-full py-2 px-5 text-sm mt-2">Send</button>
            </div>
        </div>
    );
};

export default Chat;