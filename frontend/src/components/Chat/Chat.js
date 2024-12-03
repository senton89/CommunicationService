import React, {useEffect, useState} from 'react';
import UserList from "../User/UserList";
import ChatList from "./ChatList";

const Chat = ( contact_id ) => {
    const users = UserList(); // Получаем список пользователей
    const userId = JSON.parse(localStorage.getItem('user')).id; // Получаем userId из localStorage

    // Создаем объект для быстрого поиска username по sender_id
    const userMap = users.reduce((acc, user) => {
        acc[user.id] = user.username;
        return acc;
    }, {});

    const [messages, setMessages] = useState(ChatList(Number(contact_id.contact_id))); // Получаем сообщения для выбранного пользователя
    const [messageInput, setMessageInput] = useState('');
    const [editIndex, setEditIndex] = useState(-1);

    console.log(messages);
    const handleSendMessage = () => {
        const trimmedMessage = messageInput.trim();
        if (trimmedMessage) {
            const newMessage = {
                sender_id: userId,
                receiver_id: Number(contact_id.contact_id), // Предполагаем, что contact_id - это id пользователя, с которым идет чат
                content: trimmedMessage,
                sent_at: new Date().toISOString() // Записываем текущее время
            };
            if (editIndex === -1) {
                setMessages([...messages, newMessage]);
            } else {
                const updatedMessages = [...messages];
                updatedMessages[editIndex] = newMessage;
                setMessages(updatedMessages);
                setEditIndex(-1);
            }
            setMessageInput('');
        }
    };

    const handleEditMessage = (index) => {
        setMessageInput(messages[index].content);
        setEditIndex(index);
    };

    const handleDeleteMessage = () => {
        if (editIndex !== -1) {
            const updatedMessages = messages.filter((_, index) => index !== editIndex);
            setMessages(updatedMessages);
            setEditIndex(-1);
            setMessageInput('');
        }
    };

    return (
        <div className="container">
            <div className="header">
                <h1 className="text-xl font-semibold">Личные Сообщения с {userMap[contact_id.contact_id]}</h1>
                <button id="newMessageBtn" className="bg-blue-500 text-white rounded-lg px-4 py-2">Новое Сообщение</button>
            </div>

            <div className="message-list mt-5" id="messageList">
                {messages.map((message, index) => (
                    <div key={index} className="message-item flex justify-between">
                        <div className="contact-info ml-3">
                            <p className="name font-semibold text-black">
                                {message.sender_id !== userId ? userMap[message.sender_id] : userMap[message.receiver_id]}
                            </p>
                            <p className="details text-xs text-gray-500">{message.content}</p>
                        </div>
                        <div className="contact-time text-xs text-gray-500">
                            {new Date(message.sent_at).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
                        </div>
                    </div>
                ))}
            </div>

            <div className="mt-5">
                <textarea
                    id="messageInput"
                    className="w-full border rounded-lg p-2"
                    rows="4"
                    placeholder="Введите ваше сообщение..."
                    value={messageInput}
                    onChange={(e) => setMessageInput(e.target.value)}
                ></textarea>
                <div className="flex justify-between mt-2">
                    <button id="sendMessageBtn" className="bg-green-500 text-white rounded-lg px-4 py-2" onClick={handleSendMessage}>
                        Отправить
                    </button>
                    {editIndex !== -1 && (
                        <>
                            <button className="bg-yellow-500 text-white rounded-lg px-4 py-2" onClick={handleSendMessage}>
                                Редактировать
                            </button>
                            <button className="bg-red-500 text-white rounded-lg px-4 py-2" onClick={handleDeleteMessage}>
                                Удалить
                            </button>
                        </>
                    )}
                </div>
            </div>
        </div>
    );
};

export default Chat;