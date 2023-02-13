import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import redirect401 from "./redirect401";
import axios from "axios";

axios.interceptors.response.use(v => v, e => {
    if (e.response.status === 401 && window.location.pathname !== '/login') {
        redirect401()
    }
})

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <App/>
    </React.StrictMode>
);
