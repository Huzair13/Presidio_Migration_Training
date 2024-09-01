import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/common/Header'; // Corrected case
import Footer from './components/common/Footer'; 
import Login from './components/Login/Login';
import Register from './components/Register/Register';
import TodoList from './components/Todo/TodoList';
import TodoForm from './components/Todo/TodoForm'; // Corrected case

const App = () => (
  <Router>
    <Header />
    <main className="container">
      <Routes>
        <Route path="/" element={<TodoList />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/todos" element={<TodoList />} />
        <Route path="/todos/new" element={<TodoForm />} />
        <Route path="/todos/edit/:id" element={<TodoForm />} />
      </Routes>
    </main>
    <Footer />
  </Router>
);

export default App;
