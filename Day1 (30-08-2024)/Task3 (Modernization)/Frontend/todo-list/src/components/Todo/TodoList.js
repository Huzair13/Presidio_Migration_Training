import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

const TodoList = () => {
  const [todos, setTodos] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7272/api/Todo')
      .then(response => {
        setTodos(response.data);
      })
      .catch(error => {
        console.error('Error fetching todos:', error);
      });
  }, []);

  const handleDelete = (id) => {
    if (window.confirm('Are you sure you want to delete this todo?')) {
      axios.delete(`https://localhost:7272/api/Todo/${id}`)
        .then(() => {
          setTodos(todos.filter(todo => todo.id !== id));
        })
        .catch(error => {
          console.error('Error deleting todo:', error);
        });
    }
  };

  return (
    <div className="container">
      <h3 className="text-center">List of Todos</h3>
      <hr />
      <div className="text-left">
        <Link to="/todos/new" className="btn btn-success">Add Todo</Link>
      </div>
      <br />
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Title</th>
            <th>Target Date</th>
            <th>Todo Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {todos.map(todo => (
            <tr key={todo.id}>
              <td>{todo.title}</td>
              <td>{todo.targetDate}</td>
              <td>{todo.isDone ? 'Complete' : 'In Progress'}</td>
              <td>
                <Link to={`/todos/edit/${todo.id}`} className="btn btn-primary">Edit</Link>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <button 
                  onClick={() => handleDelete(todo.id)} 
                  className="btn btn-danger"
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default TodoList;
