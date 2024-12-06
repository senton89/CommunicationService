import React, { useEffect, useState } from 'react';
import MessageService from '../../services/MessageService';
import './MessagesList.css'

const MessageList = ( userId ) => {
    const [messages, setMessages] = useState([]);

    useEffect(() => {
        const fetchMessages = async () => {
            try {
                const data = await MessageService.getMessagesByUserId(userId);
                setMessages(data);
            } catch (error) {
                console.error("Error fetching messages:", error);
            }
        };

        fetchMessages();
    }, [userId]);

    return (messages);

};

export default MessageList;