import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Header/Navbar';
import UserList from "./components/User/UserList";
import PostList from './components/Posts/PostList';
import PostDetail from './components/Posts/PostDetail';
import UserProfile from './components/User/UserProfile';
import MessageList from './components/Messages/MessageList';
import Footer from './components/Footer/Footer';
import Sidebar from "./components/Header/Sidebar";

const App = () => {
  return (
      <Router>
        <div>
          <Navbar/>
          <Routes>
            <Route path="/" exact element={<PostList/>} />
            <Route path="/users" element={<UserList/>} />
            <Route path="/posts/:id" element={<PostDetail/>} />
            <Route path="/posts" element={<PostList/>} />
            <Route path="/users/:id" element={<UserProfile/>} />
            <Route path="/messages" element={<MessageList/>} />
          </Routes>
            <Sidebar/>
          <Footer/>
        </div>
      </Router>
  );
};

export default App;