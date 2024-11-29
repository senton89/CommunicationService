// CommentService.js

const API_URL = 'http://localhost:5005/api/comments'; // Замените на ваш URL API

// Получить все комментарии
export const fetchComments = async () => {
    const response = await fetch(API_URL);
    if (!response.ok) {
        throw new Error('Ошибка при получении комментариев');
    }
    return await response.json();
};

// Добавить новый комментарий
export const addComment = async (comment) => {
    const response = await fetch(API_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(comment),
    });
    if (!response.ok) {
        throw new Error('Ошибка при добавлении комментария');
    }
    return await response.json();
};

// Обновить существующий комментарий
export const updateComment = async (commentId, updatedComment) => {
    const response = await fetch(`${API_URL}/${commentId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedComment),
    });
    if (!response.ok) {
        throw new Error('Ошибка при обновлении комментария');
    }
    return await response.json();
};

// Удалить комментарий
export const deleteComment = async (commentId) => {
    const response = await fetch(`${API_URL}/${commentId}`, {
        method: 'DELETE',
    });
    if (!response.ok) {
        throw new Error('Ошибка при удалении комментария');
    }
    return await response.json();
};