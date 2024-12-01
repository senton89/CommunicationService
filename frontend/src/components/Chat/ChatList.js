import React, { useEffect, useState } from 'react';
import MessageService from '../../services/MessageService';

const ChatList = (contact_id) => {
    const [messages, setMessages] = useState([]);
    const user = JSON.parse(localStorage.getItem('user'));

    useEffect(() => {
        const fetchMessages = async () => {
            try {
                const data = await MessageService.getMessages(user.id,contact_id);
                setMessages(data);
            } catch (error) {
                console.error("Error fetching messages:", error);
            }
        };

        fetchMessages();
    }, [contact_id]);

    return (messages);

};

export default ChatList;