import React, { useState } from 'react';
import { userHistory } from 'react-router-dom';
import './styles.css';
import logoImage from '../../assets/logo.svg';
import padlock from '../../assets/padlock.png';
import api from '../../services/api';

export default function Login() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const history = {};

    async function login(evt) {
        evt.preventDefault();

        const data = {
            username, password
        };

        console.log(`Dados de login: username = ${data.username}, password = ${data.password}`);

        try {
            const response = await api.post('api/v1/auth/signin', data);
            
            // armazenando no navegador
            localStorage.setItem('username', username);
            localStorage.setItem('accesstoken', response.data.accessToken);
            localStorage.setItem('refreshtoken', response.data.refreshToken);

            // Tudo dando certo, retorna para a tela de listagem de books
            history.push('/books');
        } catch (err) {
            alert('Login failed! Try again!');
        }
    }

    // Como "class" é uma palavra reservada do JS
    // devemos usar "className".
    return (
        <div className="login-container">
            <section className="form">
                <img src={logoImage} alt="REST API Logo" />

                <form onSubmit={login}>
                    <h1>Access your account</h1>
                    
                    <input placeholder="Username" 
                        value={username}
                        onChange={e => setUsername(e.target.value)} />

                    <input type="password" 
                        placeholder="Password" 
                        value={password} 
                        onChange={e => setPassword(e.target.value)} />

                    <button className="button" type="submit">Login</button>
                </form>
            </section>

            <img src={padlock} alt="Login" />
        </div>
    );
}