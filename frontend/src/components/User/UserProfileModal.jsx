import React from 'react';
import UserProfile from './UserProfile'; // Импортируем компонент профиля

const UserProfileModal = ({ isOpen, onClose, userId }) => {
    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <button onClick={onClose}>Close</button>
                <UserProfile userId={userId} />
            </div>
        </div>
    );
};

export default UserProfileModal;