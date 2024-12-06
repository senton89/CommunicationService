import React, { useEffect, useState } from 'react';
import TopicService from '../../services/TopicService'; // Импортируем TopicService

const TopicsList = () => {
    const [topics, setTopics] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchTopics = async () => {
            try {
                const data = await TopicService.getTopics();
                setTopics(data);
            } catch (err) {
                setError('Не удалось загрузить топики');
            }
        };

        fetchTopics();
    }, []);

    if (error) {
        return <p>{error}</p>;
    }

    return (
        topics
    );
};

export default TopicsList;