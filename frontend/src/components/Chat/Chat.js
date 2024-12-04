import React, {useEffect, useRef, useState} from 'react';
import UserList from "../User/UserList";
import ChatList from "./ChatList";
import './Chat.css'
import MessageService from "../../services/MessageService";
import {useNavigate, useNavigation} from "react-router-dom";

const Chat = ( contact_id ) => {
    const users = UserList(); // Получаем список пользователей
    const userId = JSON.parse(localStorage.getItem('user')).id; // Получаем userId из localStorage
    const messagesList = ChatList(Number(contact_id.contact_id));
    const navigate = useNavigate();
    const messagesEndRef = useRef(null); // Создаем реф для контейнера сообщений


    // Создаем объект для быстрого поиска username по sender_id
    const userMap = users.reduce((acc, user) => {
        acc[user.id] = user.username;
        return acc;
    }, {});

    const [messages, setMessages] = useState([]); // Получаем сообщения для выбранного пользователя
    const [messageInput, setMessageInput] = useState('');
    const [editIndex, setEditIndex] = useState(-1);

    useEffect(() => {
        setMessages(messagesList);
    }, );
    useEffect(() => {
        // Прокручиваем вниз, когда сообщения обновляются
        if (messagesEndRef.current) {
            messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
        }
    }, [messages]);
    const handleSendMessage = () => {
        const trimmedMessage = messageInput.trim();
        if (trimmedMessage) {
            const newMessage = {
                sender_id: userId,
                receiver_id: Number(contact_id.contact_id), // Предполагаем, что contact_id - это id пользователя, с которым идет чат
                content: trimmedMessage,
                sent_at: new Date().toISOString() // Записываем текущее время
            };
            MessageService.sendMessage(newMessage);
            if (editIndex === -1) {
                setMessages([...messages, newMessage]);
            } else {
                const updatedMessages = [...messages];
                updatedMessages[editIndex] = newMessage;
                setMessages(updatedMessages);
                setEditIndex(-1);
                MessageService.sendMessage(newMessage);
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

    const handleReturn = () =>{
        navigate(`/main`);
    }
    console.log(userMap);

    return (
        <div className="chat-container">
            <div className="chat-header">
                <h1 className="chat-title">Личные Сообщения с {userMap[contact_id.contact_id]}</h1>
            </div>

            <div className="chat-message-list mt-5" id="messageList">
                {messages.map((message, index) => (
                    <div key={index} className="chat-message-item flex justify-between">
                        <div className="chat-contact-info ml-3">
                            <p className="chat-name font-semibold text-black">
                                {message.sender_id !== userId ? userMap[message.sender_id] : "You"}
                            </p>
                            <div className="chat-message-details flex justify-between items-center">
                                <p className="chat-details text-xs text-gray-500">{message.content}</p>
                                <span className="chat-contact-time text-xs text-gray-500 ml-2">
                                    {new Date(message.sent_at).toLocaleTimeString([], {
                                        hour: '2-digit',
                                        minute: '2-digit'
                                    })}
                                </span>
                            </div>
                        </div>
                    </div>
                ))}
                <div ref={messagesEndRef}/>
            </div>

            <div className="mt-5">
                <textarea
                    id="messageInput"
                    className="chat-message-input w-full border rounded-lg p-2"
                    rows="4"
                    placeholder="Введите ваше сообщение..."
                    value={messageInput}
                    onChange={(e) => setMessageInput(e.target.value)}
                ></textarea>
                <div className="chat-btn-container flex justify-between mt-2">
                    <button id=" sendMessageBtn"
                            className="chat-send-message-btn bg-green-500 text-white rounded-lg px-4 py-2"
                            onClick={handleSendMessage}>
                        Отправить
                    </button>
                    <button
                            className="chat-return-btn bg-white text-black rounded-lg py-1 px-4 text-sm"
                            onClick={handleReturn}>
                        Назад
                    </button>
                    {editIndex !== -1 && (
                        <div>
                            <button className="chat-edit-message-btn bg-yellow-500 text-white rounded-lg px-4 py-2"
                                    onClick={handleSendMessage}>
                                Редактировать
                            </button>
                            <button className="chat-delete-message-btn bg-red-500 text-white rounded-lg px-4 py-2"
                                    onClick={handleDeleteMessage}>
                                Удалить
                            </button>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default Chat;