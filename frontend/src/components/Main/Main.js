import React, {useEffect, useState} from 'react';
import {Link, useNavigate} from 'react-router-dom'; // Импортируем useHistory
import "./Main.css";
import MessageList from "../Messages/MessageList";
import UserList from "../User/UserList";
import PostList from "../Posts/PostList";
import { FaSearch } from "react-icons/fa";
import {UserProvider} from "../../context/UserContext";
import * as PropTypes from "prop-types";
import { TransitionGroup, CSSTransition } from 'react-transition-group';
import TopicsList from "../Posts/TopicsList";

TransitionGroup.propTypes = {children: PropTypes.node};
// создание и редактирование заголовков
const Main = ({ userId }) => {
    const navigate = useNavigate(); // Используем useHistory для навигации
    const [searchTerm, setSearchTerm] = useState('');
    const [filteredUsers, setFilteredUsers] = useState([]);
    const [noUserFound,setNoUserFound] = useState(false);
    const [postSearchTerm, setPostSearchTerm] = useState(''); // Состояние для поиска постов
    const [filteredPosts, setFilteredPosts] = useState([]); // Состояние для отфильтрованных постов
    const messages = MessageList(userId);
    const users = UserList();
    const topics = TopicsList();
    const posts = PostList();
    const user = JSON.parse(localStorage.getItem('user'));

    if(user===null){
        UserProvider.logout(); // Сообщение о необходимости входа
        navigate('/login'); // Перенаправление на страницу входа
    }

    // Создаем объект для быстрого поиска username по sender_id
    const userMap = Array.isArray(users) ? users.reduce((acc, user) => {
        acc[user.id] = user.username;
        return acc;
    }, {}) : {};

    const topicsMap = Array.isArray(topics) ? topics.reduce((acc, topic) => {
        acc[topic.id] = topic.title;
        return acc;
    }, {}) : {};

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
            const filtered = users.filter(u => (u.username.toLowerCase().includes(value.toLowerCase()) && u.username!==user.username));
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
    const handleSearchSubmit = () => {
        const filtered = messages.filter(message => {
            const contactId = message.sender_id !== userId ? message.sender_id : message.receiver_id;
            if(searchTerm === "") {
                return [];
            }
            return userMap[contactId].toLowerCase().includes(searchTerm.toLowerCase());
        });

        if (filtered.length > 0) {
            if(filtered.length === 1) {
                const contactId = filtered[0].sender_id !== userId ? filtered[0].sender_id : filtered[0].receiver_id;
                navigate(`/chat/${contactId}`);
            }
            setNoUserFound(true);
        }
    };
    // Обработчик для поиска постов
    const handlePostSearchChange = (e) => {
        const value = e.target.value;
        setPostSearchTerm(value);
        if (value) {
            const filtered = posts.filter(post => post.content.toLowerCase().includes(value.toLowerCase()));
            setFilteredPosts(filtered);
        } else {
            setFilteredPosts([]);
        }
    };

    const handlePostSearchSubmit = () => {
        const filtered = posts.filter(post => post.content.toLowerCase().includes(postSearchTerm.toLowerCase()));
        setFilteredPosts(filtered);
    };
    const handlePostCreate = () => {
        navigate("/post-create");
    };

    return (
        <div className="container mx-auto my-12 bg-white rounded-2xl shadow-lg flex p-4">
            <div className="main-content flex-2 pr-3 border-r border-gray-200">
                <div className="header flex justify-between items-center pb-5 border-b border-gray-200">
                    <div className="header-left flex items-center">
                        <img alt="Profile picture" className="rounded-full" height="50" src="https://placehold.co/50x50"
                             width="50" onClick={handleProfileClick}/>
                        <div className="header-username">
                            <p className="font-semibold" onClick={handleProfileClick}>{user.username}</p>
                        </div>
                    </div>
                    <div>
                        <button className="bg-[#f5e9e2] border border-gray-200 rounded-full py-1 px-4 text-sm"
                                onClick={handleStartChatClick}>
                            Start Chat
                        </button>
                    </div>
                </div>
                <div className="section-title font-semibold my-5">Contacts</div>
                <div className="search-bar flex items-center py-2">
                    <input placeholder={noUserFound ?
                        "There is no user with this username" : "Search Contacts"} type="text"
                           className="flex-grow border-b border-gray-200 py-1 px-2 text-sm"
                           value={searchTerm}
                           onChange={handleSearchChange}/>
                    <button className="bg-[#f5e9e2] border border-gray-200 rounded-full p-2 ml-4"
                            onClick={handleSearchSubmit}>
                        <FaSearch/>
                    </button>
                </div>
                {messages.map((message, index) => (
                    <Link to={`/chat/${message.sender_id !== userId ? message.sender_id : message.receiver_id}`}
                          key={index}
                          className="contact-item flex justify-between items-center py-2 border-b border-gray-200">
                        <img alt="Contact picture" className="rounded-full" height="40" src="https://placehold.co/40x40"
                             width="40"/>
                        <div className="contact-info ml-3">
                            <p className="name font-semibold text-black">
                                {message.sender_id !== userId ? userMap[message.sender_id] : userMap[message.receiver_id]}
                            </p>
                            <p className="details text-xs text-gray-500">{message.content}</p>
                        </div>
                        <div
                            className="contact-time text-xs text-gray-500">{new Date(message.sent_at).toLocaleTimeString([], {
                            hour: '2-digit',
                            minute: '2-digit'
                        })}</div>
                    </Link>
                ))}
            </div>
            <div className="forum-content flex-1 pl-3">
                <div className="forum-section mt-5">
                    <div className="section-title font-semibold">Forum</div>
                    <div className="forum-items-container">
                        <div className="search-bar flex items-center py-2">
                            <input placeholder="Search Posts" type="text"
                                   className="flex-grow border-b border-gray-200 py-1 px-2 text-sm"
                                   value={postSearchTerm}
                                   onChange={handlePostSearchChange}/>
                            <button className="bg-[#f5e9e2] border border-gray-200 rounded-full p-2 ml-4"
                                    onClick={handlePostSearchSubmit}>
                                <FaSearch/>
                            </button>
                        </div>
                        <TransitionGroup>
                        {filteredPosts.map((post, index) => (
                            <CSSTransition key={index} timeout={300} classNames="forum-item">
                                <Link to={`/comments/${post.id}`} key={index}
                                      className="forum-item flex justify-between items-center py-2 border-b border-gray-200">
                            <div key={index}
                                 className="forum-item flex justify-between items-center py-2 border-b border-gray-200">
                                <img alt="Forum picture" className="rounded-full" height="40"
                                     src="https://placehold.co/40x40" width="40"/>
                                <div className="forum-info ml-3">
                                    <p className="name font-semibold">{topicsMap[post.topic_id]}</p>
                                    <p className="details text-xs text-gray-500">{post.content}</p>
                                </div>
                                <div className="forum-actions flex items-center">
                                    <i className="fas fa-heart text-gray-500"></i>
                                    <i className="fas fa-plus text-gray-500 ml-2"></i>
                                </div>
                            </div>
                                </Link>
                            </CSSTransition>
                            ))}
                        </TransitionGroup>
                    </div>
                </div>
                <div className="footer text-center mt-5">
                    <button className="bg-gray-800 text-white rounded-full py-2 px-5 text-sm"
                            onClick={handlePostCreate}>Create post</button>
                </div>
            </div>
            {isModalOpen && (
                <div className="modal-main fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
                    <div className="modal-content-main bg-white rounded-lg p-5">
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
                                <li key={user.id} onClick={() => handleUserSelect(user.id)}
                                    className="user-item cursor-pointer hover:bg-gray-200 p-2 rounded-full bg-white">
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