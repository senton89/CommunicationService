import React, { useState, useEffect } from 'react';
import CommentService from "../../services/CommentService";
import './Comment.css';
import {useNavigate} from "react-router-dom";
import UserList from "../User/UserList"; // Импортируйте стили, если они есть

const CommentForm = ({ postId}) => {
    const [comments, setComments] = useState([]);
    const [Content, setContent] = useState('');
    const navigate = useNavigate();
    const users = UserList();

    const userMap = Array.isArray(users) ? users.reduce((acc, user) => {
        acc[user.id] = user.username;
        return acc;
    }, {}) : {};

    useEffect(() => {
        const fetchComments = async () => {
            const fetchedComments = await CommentService.getComments(postId);
            setComments(fetchedComments);
        };

        fetchComments();
    }, [postId]);

    const handleCommentChange = (e) => {
        setContent(e.target.value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newComment = {
            content: Content,
            postId: postId,
            author_id: JSON.parse(localStorage.getItem('user')).id
        }
        await CommentService.createComment(postId, newComment); // Предполагается, что у вас есть метод для создания комментария
        setContent(''); // Очистить поле ввода
        // Обновить список комментариев
        const updatedComments = await CommentService.getComments(postId);
        setComments(updatedComments);
    };

    const handleReturn = () =>{
        navigate('/');
    }

    return (
        <div className="modal">
            <div className="modal-content">
                <span className="close" onClick={handleReturn}>&times;</span>
                <h2>Comments</h2>
                <div className="comment-list">
                    {comments.map(comment => (
                        <div key={comment.id} className="comment">
                            <p className="author">{userMap[comment.author_id]}</p>
                            <p>{comment.content}</p>
                        </div>
                    ))}
                </div>
                <form onSubmit={handleSubmit} className="comment-form">
                    <textarea
                        value={Content}
                        onChange={handleCommentChange}
                        className="border border-gray-300 rounded-lg w-full p-2"
                        rows="4"
                        placeholder="Write your comment here..."
                        required
                    />
                    <button type="submit" className="create-button bg-gray-800 text-white rounded-full py-2 px-5 text-sm mt-2">
                        Add Comment
                    </button>
                </form>
            </div>
        </div>
    );
};

export default CommentForm;