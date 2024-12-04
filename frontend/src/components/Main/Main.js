import React, { useState } from 'react';
import {Link, useNavigate} from 'react-router-dom'; // Импортируем useHistory
import "./Main.css";
import MessageList from "../Messages/MessageList";
import UserList from "../User/UserList";
import PostList from "../Posts/PostList";
import { FaSearch } from "react-icons/fa";
import {UserProvider} from "../../context/UserContext";

//TODO довести до ума мессенджер между пользователями,
// при нажатии на пост, чтобы открывалась форма, где видны комментарии и можно написать свой,
// настройка профиля,
// регистрация пользователя,
// создание и редактирование заголовков
const Main = ({ userId }) => {
    const user = JSON.parse(localStorage.getItem('user'));
    const navigate = useNavigate(); // Используем useHistory для навигации
    const [searchTerm, setSearchTerm] = useState('');
    const [filteredUsers, setFilteredUsers] = useState([]);


    if (!user) {
        // Обработка отсутствия пользователя
        UserProvider.logout(); // Сообщение о необходимости входа
    }

    const [isModalOpen, setModalOpen] = useState(false);
    const handleEditClick = () => {
        setModalOpen(true);
    };

    const handleStartChatClick = () => {
        setModalOpen(true);
    };

    const handleSearchChange = (e) => {
        const value = e.target.value;
        setSearchTerm(value);
        if (value) {
            const filtered = users.filter(u => u.username.toLowerCase().includes(value.toLowerCase()));
            setFilteredUsers(filtered);
        } else {
            setFilteredUsers([]);
        }
    };
    const handleUserSelect = (selectedUserId) => {
        setModalOpen(false);
        navigate(`/chat/${selectedUserId}`);
    };
    const handleProfileClick = () => {
        // Переход на страницу профиля с передачей данных
        navigate(`/user-profile?name=${encodeURIComponent(user.username)}&email=${encodeURIComponent(user.email)}`);
    };


    const messages = MessageList(userId);
    const users = UserList();
    const posts = PostList();

    // Создаем объект для быстрого поиска username по sender_id
    const userMap = Array.isArray(users) ? users.reduce((acc, user) => {
        acc[user.id] = user.username;
        return acc;
    }, {}) : {};

    return (
        <div className="container mx-auto my-12 bg-white rounded-2xl shadow-lg flex p-4">
            <div className="main-content flex-2 pr-3 border-r border-gray-200">
                <div className="header flex justify-between items-center pb-5 border-b border-gray-200">
                    <div className="header-left flex items-center">
                        <img alt="Profile picture" className="rounded-full" height="50" src="https://placehold.co/50x50" width="50" onClick={handleProfileClick} />
                        <div className="header-username">
                            <p className="font-semibold" onClick={handleProfileClick}>{user.username}</p>
                        </div>
                    </div>
                    <div>
                        <button className="bg-[#f5e9e2] border border-gray-200 rounded-full py-1 px-4 text-sm" onClick={handleStartChatClick}>
                            Start Chat</button>
                    </div>
                </div>
                <div className="section-title font-semibold my-5">Contacts</div>
                {messages.map((message, index) => (
                    <Link to={`/chat/${message.sender_id !== userId ? message.sender_id : message.receiver_id}`} key={index} className="contact-item flex justify-between items-center py-2 border-b border-gray-200">
                        <img alt="Contact picture" className="rounded-full" height="40" src="https://placehold.co/40x40" width="40" />
                        <div className="contact-info ml-3">
                            <p className="name font-semibold text-black">
                                {message.sender_id !== userId ? userMap[message.sender_id] : userMap[message.receiver_id]}
                            </p>
                            <p className="details text-xs text-gray-500">{message.content}</p>
                        </div>
                        <div className="contact-time text-xs text-gray-500">{new Date(message.sent_at).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}</div>
                    </Link>
                ))}
                <div className="search-bar flex items-center py-2">
                    <input placeholder="Search Contacts" type="text" className="flex-grow border-b border-gray-200 py-1 px-2 text-sm" />
                    <button className="bg-[#f5e9e2] border border-gray-200 rounded-full p-2 ml-4">
                        <FaSearch />
                    </button>
                </div>
            </div>
            <div className="forum-content flex-1 pl-3">
                <div className="forum-section mt-5">
                    <div className="section-title font-semibold">Forum</div>
                    <div className="forum -items-container">
                        {posts.map((post, index) => (
                            <div key={index} className="forum-item flex justify-between items-center py-2 border-b border-gray-200">
                                <img alt="Forum picture" className="rounded-full" height="40" src="https://placehold.co/40x40" width="40" />
                                <div className="forum-info ml-3">
                                    <p className="name font-semibold">{post.topic_id}</p>
                                    <p className="details text-xs text-gray-500">{post.content}</p>
                                </div>
                                <div className="forum-actions flex items-center">
                                    <i className="fas fa-heart text-gray-500"></i>
                                    <i className="fas fa-plus text-gray-500 ml-2"></i>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
                <div className="footer text-center mt-5">
                    <button className="bg-gray-800 text-white rounded-full py-2 px-5 text-sm">Search for posts</button>
                </div>
            </div>
            {isModalOpen && (
                <div className="modal fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
                    <div className="modal-content bg-white rounded-lg p-5">
                        <span className="close cursor-pointer" onClick={() => setModalOpen(false)}>&times;</span>
                        <input
                            type="text"
                            placeholder="Search for a user"
                            value={searchTerm}
                            onChange={handleSearchChange}
                            className="search-input border-b border-gray-300 py-2 px-3 w-full"
                        />
                        <ul className="user-list mt-2">
                            {filteredUsers.map(user => (
                                <li key={user.id} onClick={() => handleUserSelect(user.id)} className="user-item cursor-pointer hover:bg-gray-200 p-2">
                                    {user.username}
                                </li>
                            ))}
                        </ul>
                    </div>
                </div>
            )}
        </div>
    );
};

export default Main;