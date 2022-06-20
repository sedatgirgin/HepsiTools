import React, {useState} from 'react';
import "./Login.css";
import {useHistory, withRouter} from "react-router-dom/cjs/react-router-dom";
import {Link} from "react-router-dom";
import {connect} from "react-redux";
import {loginUser} from "../../../actions/auth";

const Login = (props) => {

    const history = useHistory();

    const location = {
        pathname: '/register',
        state: {fromDashboard: true}
    }

    let register = () => {
        history.push('/register');
    };

    const [state, setState] = useState({
        Email: "",
        Password: "",
        error: false
    });

    const handleSubmit = (event) => {
        event.preventDefault();
        const { email, password } = state;
        props
            .dispatchLoginUser({ email, password })
            .then(() => props.history.push("/"))
            .catch(() => setState({ error: true }));
    };

    const handleChange = (event) => {
        setState({
            [event.target.name]: event.target.value
        });
    };

    return (
        <div className="login-container">
            <div className="login-card">
                <div className="login-title">Giriş</div>
                <label htmlFor="userName" className="input-label">Kullanıcı Adı:</label>
                <input className="login-input" name="Email" type="text" value={state.email} onChange={handleChange}/>
                <label htmlFor="password" className="input-label">Şifre:</label>
                <input className="login-input" name="Password" type="password" value={state.password} onChange={handleChange}/>
                <button className="primary-btn login-btn" onClick={handleSubmit}>Giriş</button>
                <label>{state.error}</label>
                <label className="input-label"> Kayıtlı değilsen üye olabilirsin</label>
                <Link className="secondary-btn" to="/register">Kaydol</Link>
            </div>
        </div>
    );
};

const mapDispatchToProps = (dispatch) => {
    return {
        dispatchLoginUser: (credentials) =>  loginUser(dispatch)(credentials)
    };
};

export default connect(null, mapDispatchToProps)(Login);