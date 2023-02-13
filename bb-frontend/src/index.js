import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import axios from "axios";

axios.interceptors.response.use(v => v, e => {
    if (e.response.status === 401 && window.location.pathname !== '/login') {
        localStorage.removeItem('token')
        window.location.href = '/login'
    }
})

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <App/>
    </React.StrictMode>
);
