import React, {useState} from 'react';
import "./Login.css";
import {Link, withRouter} from "react-router-dom";
import {connect} from "react-redux";
import {loginUser} from "../../../actions/auth";

const Login = (props) => {

    const location = {
        pathname: '/register',
        state: {fromDashboard: true}
    }

    let register = () => {
        props.history.push('/register');
    };

    const [state, setState] = useState({
        username: "",
        password: "",
        error: false
    });

    const handleSubmit = (event) => {
        event.preventDefault();
        props
            .dispatchLoginUser(state)
            .then(() => {
                console.log("then çalıştı.")
                props.history.push("/panel")
            })
            .catch(() => setState({error: true}));

    };

    const handleChangeUserName = (e) => {
        console.log("username", e.target.value);
        setState({
            "password": state.password,
            "username": e.target.value
        });
        console.log("state", state);
    };

    const handleChangePassword = (e) => {
        console.log("password", e.target.value);
        setState({
            "password": e.target.value,
            "username": state.username
        });
        console.log("state", state);
    };

    return (
        <div className="login-container">
            <div className="login-card">
                <div className="login-title">Giriş</div>
                <label htmlFor="userName" className="input-label">Kullanıcı Adı:</label>
                <input className="login-input" name="Email" type="text" value={state.email}
                       onChange={handleChangeUserName}/>
                <label htmlFor="password" className="input-label">Şifre:</label>
                <input className="login-input" name="Password" type="password" value={state.password}
                       onChange={handleChangePassword}/>
                <button className="primary-btn login-btn" onClick={handleSubmit}>Giriş</button>
                <label>{state.error}</label>
                <label className="input-label"> Kayıtlı değilsen üye olabilirsin</label>
                <Link className="secondary-btn" to="/register">Kaydol</Link>
            </div>
        </div>
    );
};

const mapStateToProps = (state) => {
    return {
        loggedIn: state.loggedIn,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        dispatchLoginUser: (credentials) => dispatch(loginUser(credentials))
    };
};

export default connect(null, mapDispatchToProps)(withRouter(Login));