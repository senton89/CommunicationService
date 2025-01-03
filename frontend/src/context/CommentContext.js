import React, { createContext, useContext, useState } from 'react';

// Создаем контекст для комментариев
const CommentContext = createContext();

// Провайдер для комментариев
export const CommentProvider = ({ children }) => {
    const [comments, setComments] = useState([]);

    const addComment = (comment) => {
        setComments((prevComments) => [...prevComments, comment]);
    };

    const removeComment = (commentId) => {
        setComments((prevComments) => prevComments.filter(comment => comment.id !== commentId));
    };

    const updateComment = (updatedComment) => {
        setComments((prevComments) =>
            prevComments.map(comment =>
                comment.id === updatedComment.id ? updatedComment : comment
            )
        );
    };

    return (
        <CommentContext.Provider value={{ comments, addComment, removeComment, updateComment }}>
            {children}
        </CommentContext.Provider>
    );
};

// Хук для использования комментариев
export const useComments = () => {
    return useContext(CommentContext);
};