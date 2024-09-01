import React from 'react';

const footerStyle = {
  position: 'fixed',
  bottom: 0,
  width: '100%',
  height: '40px',
  backgroundColor: 'tomato',
};

const copyrightStyle = {
  color: 'white',
  textAlign: 'center',
  padding: '10px 0', 
};

const Footer = () => (
  <footer style={footerStyle} className="font-small black">
    <div style={copyrightStyle} className="footer-copyright">
      &copy; 2019 Copyright:
      <a href="https://www.javaguides.net/" style={{ color: 'white' }}>
        <strong> Java Guides </strong>
      </a>
    </div>
  </footer>
);

export default Footer;
