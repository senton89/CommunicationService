// server.js или app.js
const express = require('express');
const cors = require('cors');

const app = express();
const PORT = 5005;

// Настройка CORS
app.use(cors({
    origin: 'http://localhost:3000', // Разрешить доступ только с этого источника
    methods: ['GET', 'POST', 'PUT', 'DELETE'], // Разрешенные методы
    credentials: true, // Если требуется передача куки
}));

app.use(express.json()); // Для парсинга JSON-данных

// Пример маршрутов
app.get('/api/users', (req, res) => {
    res.json([{ id: 1, name: 'User  1' }, { id: 2, name: 'User  2' }]);
});

// Другие маршруты для создания, обновления и удаления пользователей
app.post('/api/users', (req, res) => {
    const newUser  = req.body; // Получаем данные нового пользователя
    res.status(201).json(newUser ); // Возвращаем созданного пользователя
});

app.put('/api/users/:id', (req, res) => {
    const updatedUser  = req.body; // Получаем данные обновленного пользователя
    res.json(updatedUser ); // Возвращаем обновленного пользователя
});

app.delete('/api/users/:id', (req, res) => {
    const { id } = req.params;
    // Логика для удаления пользователя
    res.status(204).send(); // Возвращаем статус 204 No Content
});

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});