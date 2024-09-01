import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';

const TodoForm = () => {
  const [todo, setTodo] = useState({
    id: '',
    title: '',
    description: '',
    isDone: false,
    targetDate: '',
    username: ''  // Added username field
  });
  const [isEditMode, setIsEditMode] = useState(false);
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      axios.get(`https://localhost:7272/api/Todo/${id}`)
        .then(response => {
          setTodo({
            id: response.data.id,
            title: response.data.title,
            description: response.data.description,
            isDone: response.data.status,
            targetDate: response.data.targetDate,
            username: response.data.username // Ensure username is set
          });
          setIsEditMode(true);
        })
        .catch(error => {
          console.error('Error fetching todo:', error);
        });
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setTodo({
      ...todo,
      [name]: type === 'checkbox' ? checked : value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (isEditMode) {
        // Update request should have all fields including ID
        await axios.put(`https://localhost:7272/api/Todo/${todo.id}`, todo);
      } else {
        // For creating a new todo, ID should be omitted
        await axios.post('https://localhost:7272/api/Todo', {
          title: todo.title,
          description: todo.description,
          targetDate: todo.targetDate,
          status: todo.isDone,
          username: todo.username  // Ensure username is included
        });
      }
      navigate('/todos');
    } catch (error) {
      console.error('Error saving todo:', error);
    }
  };

  return (
    <div className="container col-md-5">
      <div className="card">
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <h2>{isEditMode ? 'Edit Todo' : 'Add New Todo'}</h2>

            {isEditMode && <input type="hidden" name="id" value={todo.id} />}

            <fieldset className="form-group">
              <label>Todo Title</label>
              <input
                type="text"
                value={todo.title}
                className="form-control"
                name="title"
                onChange={handleChange}
                required
                minLength="5"
              />
            </fieldset>

            <fieldset className="form-group">
              <label>Todo Description</label>
              <input
                type="text"
                value={todo.description}
                className="form-control"
                name="description"
                onChange={handleChange}
                minLength="5"
              />
            </fieldset>

            <fieldset className="form-group">
              <label>Todo Status</label>
              <select
                className="form-control"
                name="isDone"
                value={todo.isDone}
                onChange={handleChange}
              >
                <option value="false">In Progress</option>
                <option value="true">Complete</option>
              </select>
            </fieldset>

            <fieldset className="form-group">
              <label>Todo Target Date</label>
              <input
                type="date"
                value={todo.targetDate}
                className="form-control"
                name="targetDate"
                onChange={handleChange}
                required
              />
            </fieldset>

            <fieldset className="form-group">
              <label>Username</label>
              <input
                type="text"
                value={todo.username}
                className="form-control"
                name="username"
                onChange={handleChange}
                required
              />
            </fieldset>

            <button type="submit" className="btn btn-success">
              Save
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default TodoForm;
