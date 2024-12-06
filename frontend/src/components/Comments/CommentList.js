import React from 'react';
import PostService from "../../services/PostService";
import CommentService from "../../services/CommentService";

const CommentList = (postId) => {
    const comments = CommentService.getComments(postId)
    if (!comments || comments.length === 0) {
        return [];
    }

    return (comments);
};

export default CommentList;