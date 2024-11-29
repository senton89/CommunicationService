import React, { useEffect, useState } from 'react';
import MessageService from '../../services/MessageService';

const MessageList = () => {
    const [messages, setMessages] = useState([]);

    useEffect(() => {
        MessageService.getMessages().then(data => setMessages(data));
    }, []);

    return (
        <div>
            <h1>Messages</h1>
            <ul>
                {messages.map(message => (
                    <li key={message.id}>{message.content}</li>
                ))}
            </ul>
        </div>
    );
};

export default MessageList;