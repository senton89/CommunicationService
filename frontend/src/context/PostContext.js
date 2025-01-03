import React, { createContext, useContext, useState } from 'react';

// Создаем контекст для постов
const PostContext = createContext();

// Провайдер для постов
export const PostProvider = ({ children }) => {
    const [posts, setPosts] = useState([]);

    const addPost = (post) => {
        setPosts((prevPosts) => [...prevPosts, post]);
    };

    const removePost = (postId) => {
        setPosts((prevPosts) => prevPosts.filter(post => post.id !== postId));
    };

    const updatePost = (updatedPost) => {
        setPosts((prevPosts) =>
            prevPosts.map(post =>
                post.id === updatedPost.id ? updatedPost : post
            )
        );
    };

    return (
        <PostContext.Provider value={{ posts, addPost, removePost, updatePost }}>
            {children}
        </PostContext.Provider>
    );
};

// Хук для использования постов
export const usePosts = () => {
    return useContext(PostContext);
};