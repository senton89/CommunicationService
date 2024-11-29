import React from 'react';
import { Link } from 'react-router-dom';

const Sidebar = () => {
    return (
        <aside style={styles.sidebar}>
            <h2>Навигация</h2>
            <ul style={styles.list}>
                <li>
                    <Link to="/">Главная</Link>
                </li>
                <li>
                    <Link to="/messages">Сообщения</Link>
                </li>
                <li>
                    <Link to="/users/1">Профиль пользователя</Link>
                </li>
                <li>
                    <Link to="/posts">Все посты</Link>
                </li>
                <li>
                    <Link to="/users">Все пользователи</Link>
                </li>
            </ul>
        </aside>
    );
};

const styles = {
    sidebar: {
        width: '250px',
        padding: '20px',
        background: '#f4f4f4',
        borderRight: '1px solid #ccc',
    },
    list: {
        listStyleType: 'none',
        padding: 0,
    },
};

export default Sidebar;