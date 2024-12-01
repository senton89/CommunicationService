import React, { useEffect, useState } from 'react';
import PostService from '../../services/PostService';
import { Link } from 'react-router-dom';

const PostList = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        PostService.getPosts().then(data => setPosts(data));
    }, []);

    return (
        posts
    );
};

export default PostList;