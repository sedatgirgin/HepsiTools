import {AUTHENTICATED, NOT_AUTHENTICATED} from './actionTypes';
import axios from "axios";

const setToken = (token) => {
    localStorage.setItem("token", token);
    localStorage.setItem("lastLoginTime", new Date(Date.now()).getTime());
};

export const getToken = () => {
    const now = new Date(Date.now()).getTime();
    const timeAllowed = 1000 * 60 * 30;
    const timeSinceLastLogin = now - localStorage.getItem("lastLoginTime");
    if (timeSinceLastLogin < timeAllowed) {
        return localStorage.getItem("token");
    }
};

const deleteToken = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("lastLoginTime");
}

const serverUrl = "http://localhost:5002";

export const signupUser = (credentials) => {
    return (dispatch) => {
        return fetch(serverUrl + "/signup", {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            withCredentials: true,
            body: JSON.stringify({user: credentials})
        }).then((res) => {
            if (res.ok) {
                setToken(res.headers.get("Authorization"));
                return res
                    .json()
                    .then((userJson) =>
                        dispatch({type: AUTHENTICATED, payload: userJson})
                    );
            } else {
                return res.json().then((errors) => {
                    dispatch({type: NOT_AUTHENTICATED});
                    return Promise.reject(errors);
                });
            }
        });
    };
};

export const loginUser = (credentials) => {
    return (dispatch) => {
        return axios.request({
            url:serverUrl + "/Account/Login",
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            withCredentials:false,
            body: JSON.stringify(credentials),
        }).then((res) => {
            if (res.ok) {
                setToken(res.headers.get("Authorization"));
                return res
                    .json()
                    .then((userJson) => {
                            console.log("user", userJson);
                            dispatch({type: AUTHENTICATED, payload: userJson});
                        }
                    );
            } else {
                return res.json().then((errors) => {
                    dispatch({type: NOT_AUTHENTICATED});
                    console.error("error", errors);
                    return Promise.reject(errors);
                });
            }
        });
    };
};

export const logoutUser = () => {
    return (dispatch) => {
        return fetch(serverUrl + "/logout", {
            method: "DELETE",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
                Authorization: getToken(),
            },
        }).then((res) => {
            deleteToken()
            if (res.ok) {
                return res.json()
                    .then(() => dispatch({type: NOT_AUTHENTICATED}))
            } else {
                return res.json().then((errors) => {
                    dispatch({type: NOT_AUTHENTICATED})
                    return Promise.reject(errors)
                })
            }
        })
    }
}

export const checkAuth = () => {
    return (dispatch) => {
        return fetch(serverUrl + "/current_user", {
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
                Authorization: getToken()
            }
        }).then((res) => {
            if (res.ok) {
                return res
                    .json()
                    .then(user => {
                        user.data ? dispatch({type: AUTHENTICATED, payload: user}) : dispatch({type: NOT_AUTHENTICATED})
                    })
            } else {
                return Promise.reject(dispatch({type: NOT_AUTHENTICATED}))
            }
        });
    };
};