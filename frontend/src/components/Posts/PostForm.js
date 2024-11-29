import React, { useState } from 'react';

const PostForm = ({ onSubmit, initialPost }) => {
    const [title, setTitle] = useState(initialPost ? initialPost.title : '');
    const [content, setContent] = useState(initialPost ? initialPost.content : '');

    const handleSubmit = (e) => {
        e.preventDefault();
        if (title.trim() && content.trim()) {
            // Создаем новый объект поста
            const newPost = {
                title,
                content,
                timestamp: new Date().toISOString(),
            };

            // Вызываем функцию onSubmit для отправки поста
            onSubmit(newPost);

            // Очищаем поля ввода
            setTitle('');
            setContent('');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>
                    Заголовок:
                    <input
                        type="text"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        placeholder="Введите заголовок поста"
                        required
                    />
                </label>
            </div>
            <div>
                <label>
                    Содержание:
                    <textarea
                        value={content}
                        onChange={(e) => setContent(e.target.value)}
                        placeholder="Введите содержание поста"
                        required
                    />
                </label>
            </div>
            <button type="submit">Сохранить пост</button>
        </form>
    );
};

export default PostForm;