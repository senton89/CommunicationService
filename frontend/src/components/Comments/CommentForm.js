import React, { useState } from 'react';

const CommentForm = ({ postId, onSubmit }) => {
    const [content, setContent] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        if (content.trim()) {
            // Создаем новый комментарий
            const newComment = {
                postId: postId,
                content: content,
            };

            // Вызываем функцию onSubmit для отправки комментария
            onSubmit(newComment);

            // Очищаем поле ввода
            setContent('');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
        <textarea
            value={content}
            onChange={(e) => setContent(e.target.value)}
            placeholder="Напишите ваш комментарий..."
            required
        />
            </div>
            <button type="submit">Отправить</button>
        </form>
    );
};

export default CommentForm;