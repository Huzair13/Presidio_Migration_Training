import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => (
  <header>
    <nav className="navbar navbar-expand-md navbar-dark" style={{ backgroundColor: 'tomato' }}>
      <div>
        <Link to="/" className="navbar-brand">Todo App</Link>
      </div>
      <ul className="navbar-nav">
        <li><Link to="/todos" className="nav-link">Todos</Link></li>
      </ul>
      <ul className="navbar-nav navbar-collapse justify-content-end">
        <li><Link to="/logout" className="nav-link">Logout</Link></li>
      </ul>
    </nav>
  </header>
);

export default Header;
