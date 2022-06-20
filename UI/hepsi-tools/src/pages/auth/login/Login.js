import React from 'react';
import "./Login.css";
import {useHistory, withRouter} from "react-router-dom/cjs/react-router-dom";
import {Link} from "react-router-dom";

const Login = () => {

    const history = useHistory();

    const location = {
        pathname: '/register',
        state: {fromDashboard: true}
    }

    let register = () => {
        history.push('/register');
    };

    return (
        <div className="login-container">
            <div className="login-card">
                <div className="login-title">Giriş</div>
                <label htmlFor="userName" className="input-label">Kullanıcı Adı:</label>
                <input className="login-input" type="text"/>
                <label htmlFor="password" className="input-label">Şifre:</label>
                <input className="login-input" type="password"/>
                <button className="primary-btn login-btn">Giriş</button>
                <label className="input-label"> Kayıtlı değilsen üye olabilirsin</label>
                <Link className="secondary-btn" to="/register">Kaydol</Link>
            </div>
        </div>
    );
};

export default Login;