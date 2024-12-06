import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import PostService from "../../services/PostService";
import TopicsList from "./TopicsList";
import './Topics.css';
import TopicService from "../../services/TopicService";

const CreatePostForm = () => {
    const [title, setTitle] = useState(''); // Состояние для заголовка
    const [content, setContent] = useState('');
    const [topicId, setTopicId] = useState('');
    const [isModalOpen, setModalOpen] = useState(false);
    const navigate = useNavigate();
    const topics = TopicsList();
    const userId = JSON.parse(localStorage.getItem('user')).id;

    const handleTitleChange = (e) => {
        setTitle(e.target.value); // Обновление состояния заголовка
    };

    const handleContentChange = (e) => {
        setContent(e.target.value);
    };

    const handleTopicChange = (e) => {
        setTopicId(e.target.value);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const newPost = {
            title, // Добавляем заголовок
            content,
            author_id: userId,
            topic_id: topicId,
        };
        PostService.createPost(newPost);

        // После успешного создания поста, можно перенаправить пользователя
        navigate('/main'); // Перенаправление на главную страницу
    };

    const handleReturn = () => {
        navigate('/main'); // Перенаправление на главную страницу
    };

    const handleModalOpen = () =>{
        setModalOpen(!isModalOpen);
    }

    const handleCreateTopic = () =>{
        const newTopic = {
            author_id: userId,
            title: title
        }
        TopicService.createTopic(newTopic);
        window.location.reload()
    }
    return (
        <div className="create-post-form bg-white rounded-lg p-5 shadow-lg">
            <h2 className="text-lg font-semibold mb-4">Create a New Post</h2>
            <form onSubmit={handleSubmit}>
                <div className="mb-4">
                    <label className="block text-sm font-medium mb-1">Content</label>
                    <textarea
                        value={content}
                        onChange={handleContentChange}
                        className="border border-gray-300 rounded-lg w-full p-2"
                        rows="4"
                        placeholder="Write your post content here..."
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block text-sm font-medium mb-1">Select Topic</label>
                    <select
                        value={topicId}
                        onChange={handleTopicChange}
                        className="border border-gray-300 rounded-lg w-full p-2"
                        required
                    >
                        <option value="">Select a topic</option>
                        {topics.map(topic => (
                            <option key={topic.id} value={topic.id}>{topic.title}</option>
                        ))}
                    </select>
                </div>
                <button type="button" onClick={handleModalOpen}
                        className="create-button bg-gray-800 text-white rounded-full py-2 px-5 text-sm">
                    Create Topic
                </button>
                {isModalOpen && (
                    <div className="modal-topic mb-4">
                        <label className="text-md font-medium mb-1">New Topic</label>
                        <div className="modal-topic-content flex items-center">
                    <input
                        type="text"
                        value={title}
                        onChange={handleTitleChange}
                        className="border border-gray-300 rounded-lg w-full p-2 flex-grow"
                        placeholder="Enter post title..."
                        required
                    />
                        <button type="button"
                                onClick={handleCreateTopic}
                                className="create-button bg-gray-800 text-white rounded-full py-2 px-5 text-sm">
                            Create
                        </button>
                        </div>
                </div>)
                }
                <div className="button-container flex">
                    <button type="submit"
                            className="create-button bg-gray-800 text-white rounded-full py-2 px-5 text-sm">
                        Create Post
                    </button>
                    <button type="button" onClick={handleReturn}
                            className="return-button bg-white text-gray-800 rounded-full py-2 px-5 text-sm">
                        Return
                    </button>
                </div>
            </form>
        </div>
    );
};

export default CreatePostForm;