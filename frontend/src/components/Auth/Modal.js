// src/components/Auth/Modal.js

import React from 'react';
import './Modal.css'; // Импортируйте стили для модального окна

const Modal = ({ isOpen, onClose, children }) => {
    if (!isOpen) return null;

    return (
        <div className="modal-body mt-4">
            {children}
        </div>
    );
};

export default Modal;