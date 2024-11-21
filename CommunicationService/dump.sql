-- Создание таблицы пользователей

CREATE TABLE users (

                       id SERIAL PRIMARY KEY,

                       username VARCHAR(50) NOT NULL UNIQUE,

                       email VARCHAR(100) NOT NULL UNIQUE,

                       password_hash VARCHAR(255) NOT NULL,

                       created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                       updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                       salt bytea NOT NULL

);

-- Создание таблицы сообщений

CREATE TABLE messages (

                          id SERIAL PRIMARY KEY,

                          sender_id INT NOT NULL,

                          receiver_id INT NOT NULL,

                          content TEXT NOT NULL,

                          sent_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                          FOREIGN KEY (sender_id) REFERENCES users(id) ON DELETE CASCADE,

                          FOREIGN KEY (receiver_id) REFERENCES users(id) ON DELETE CASCADE

);

-- Создание таблицы тем форума

CREATE TABLE topics (

                        id SERIAL PRIMARY KEY,

                        title VARCHAR(255) NOT NULL,

                        author_id INT NOT NULL,

                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                        FOREIGN KEY (author_id) REFERENCES users(id) ON DELETE CASCADE

);

-- Создание таблицы постов

CREATE TABLE posts (

                       id SERIAL PRIMARY KEY,

                       topic_id INT NOT NULL,

                       content TEXT NOT NULL,

                       author_id INT NOT NULL,

                       created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                       updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                       FOREIGN KEY (topic_id) REFERENCES topics(id) ON DELETE CASCADE,

                       FOREIGN KEY (author_id) REFERENCES users(id) ON DELETE CASCADE

);

-- Создание таблицы комментариев

CREATE TABLE comments (

                          id SERIAL PRIMARY KEY,

                          post_id INT NOT NULL,

                          content TEXT NOT NULL,

                          author_id INT NOT NULL,

                          created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                          updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                          FOREIGN KEY (post_id) REFERENCES posts(id) ON DELETE CASCADE,

                          FOREIGN KEY (author_id) REFERENCES users(id) ON DELETE CASCADE

);

INSERT INTO users (username, email, password_hash, salt) VALUES
                                                             ('user1', 'user1@example.com', 'hashed_password1', 'test'),
                                                             ('user2', 'user2@example.com', 'hashed_password2', 'test'),
                                                             ('user3', 'user3@example.com', 'hashed_password3', 'test'),
                                                             ('user4', 'user4@example.com', 'hashed_password4', 'test'),
                                                             ('user5', 'user5@example.com', 'hashed_password5', 'test'),
                                                             ('user6', 'user6@example.com', 'hashed_password6', 'test'),
                                                             ('user7', 'user7@example.com', 'hashed_password7', 'test'),
                                                             ('user8', 'user8@example.com', 'hashed_password8', 'test'),
                                                             ('user9', 'user9@example.com', 'hashed_password9', 'test'),
                                                             ('user10', 'user10@example.com', 'hashed_password10', 'test');

INSERT INTO topics (title, author_id) VALUES
                                          ('First Topic', 1),
                                          ('Second Topic', 2),
                                          ('Third Topic', 3),
                                          ('Fourth Topic', 4),
                                          ('Fifth Topic', 5),
                                          ('Sixth Topic', 6),
                                          ('Seventh Topic', 7),
                                          ('Eighth Topic', 8),
                                          ('Ninth Topic', 9),
                                          ('Tenth Topic', 10);

INSERT INTO posts (id,topic_id, content, author_id) VALUES
                                                        (1,1, 'This is the content of the first post.', 1),
                                                        (2,1, 'This is a reply to the first post.', 2),
                                                        (3,2, 'Content for the second topic post.', 3),
                                                        (4,2, 'Another reply to the second post.', 4),
                                                        (5,3, 'Discussion on the third topic.', 5),
                                                        (6,4, 'Content for the fourth topic.', 6),
                                                        (7,5, 'Fifth topic post content.', 7),
                                                        (8,6, 'Sixth topic post details.', 8),
                                                        (9,7, 'Seventh topic post content goes here.', 9),
                                                        (10,8, 'Eighth topic post content.', 10);

INSERT INTO comments (post_id, content, author_id) VALUES
                                                       (1, 'This is a comment on the first post.', 2),
                                                       (1, 'Another comment on the first post.', 3),
                                                       (2, 'Comment on the second post.', 4),
                                                       (3, 'Comment on the third post.', 5),
                                                       (4, 'Comment on the fourth post.', 6),
                                                       (5, 'Comment on the fifth post.', 7),
                                                       (6, 'Comment on the sixth post.', 8),
                                                       (7, 'Comment on the seventh post.', 9),
                                                       (8, 'Comment on the eighth post.', 10),
                                                       (9, 'Final comment on the ninth post.', 1);
