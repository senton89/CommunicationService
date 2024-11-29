import React from 'react';

const CommentList = ({ comments }) => {
    if (!comments || comments.length === 0) {
        return <p>Комментариев нет.</p>;
    }

    return (
        <div>
            <h2>Комментарии</h2>
            <ul>
                {comments.map(comment => (
                    <li key={comment.id}>
                        <p>{comment.content}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default CommentList;