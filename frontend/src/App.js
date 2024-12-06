import React, { useState } from 'react';
import {BrowserRouter as Router, Navigate, Route, Routes, useParams} from 'react-router-dom';
import UserList from "./components/User/UserList";
import Login from "./components/Auth/Login";
import MessageList from './components/Messages/MessageList';
import { MessageProvider } from './context/MessageContext';
import { UserProvider, useUser  } from "./context/UserContext"; // Импортируйте ваш контекст пользователей
import Modal from './components/Auth/Modal';
import Main from "./components/Main/Main";
import Chat from "./components/Chat/Chat";
import UserProfile from "./components/User/UserProfile";
import Register from "./components/Auth/Register";
import CreatePostForm from "./components/Posts/CreatePostForm";
import CommentForm from "./components/Comments/CommentForm";

const App = () => {
    const [isModalOpen, setModalOpen] = useState((localStorage.getItem('user')===null));

    return (
        <Router>
            <UserProvider>
                <MessageProvider>
                    <div className="flex">
                        {isModalOpen ? (
                            // Если модальное окно открыто, показываем только его
                            <Modal isOpen={isModalOpen} onClose={() => setModalOpen(false)}>
                                <Register onSuccess={() => setModalOpen(false)}/>
                            </Modal>
                        ) : (
                            // Если модальное окно закрыто, показываем основной контент
                            <Routes>
                                <Route path="/register" element={<Register/>} />
                                <Route path="/login" element={<LoginWrapper/>}/>
                                <Route path="/" element={<Navigate to="/main"/>}/>
                                <Route path="/main" element={<MainWrapper/>}/>
                                <Route path="/user-profile" element={<UserProfile/>} />
                                <Route path="/chat/:contactId" element={<ChatWrapper/>}/>
                                <Route path="/users" element={<UserList/>}/>
                                <Route path="/messages/:userId" element={<MessageList/>}/>
                                <Route path="/comments/:postId" element={<CommentWrapper/>}/>
                                <Route path="/post-create" element={<CreatePostForm/>}/>
                                <Route path="*" element={<NotFound/>}/> {/* Обработка несуществующих маршрутов */}
                            </Routes>
                        )}
                    </div>
                </MessageProvider>
            </UserProvider>
        </Router>
    );
};

// Компонент для обработки несуществующих маршрутов
const NotFound = () => {
    return <h2>404 - Страница не найдена</h2>;
};

// Обертка для маршрута входа
const LoginWrapper = ({setModalOpen}) => {
    const {user} = useUser(); // Получаем информацию о пользователе из контекста
    if(user !== null)
        return user ? <Navigate to="/"/> : <Login onSuccess={() => setModalOpen(false)}/>;
};

// Обертка для маршрута Main, чтобы передать userId
const MainWrapper = () => {
    const { user } = useUser(); // Получаем информацию о пользователе из контекста
    if(user !== null)
        return user ? <Main userId={user.id}/> : <Navigate to="/" />;
};

const ChatWrapper = () => {
    const {contactId} = useParams();
        return contactId ? <Chat contact_id={contactId} /> : <Navigate to="/" />;
};
const CommentWrapper = () => {
    const {postId} = useParams();
    return postId ? <CommentForm postId={postId} /> : <Navigate to="/" />;
};
// Компонент для защищенных маршрутов
const ProtectedRoute = ({component: Component}) => {
    const {user} = useUser(); // Получаем информацию о пользователе из контекста
    return user ? <Component/> : <Navigate to="/"/>;
};

export default App;

