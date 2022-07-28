import React from "react";
import "./Input.css";

const Input = ({ type ,id ,name, label, onChange, placeHolder, value, error, wrapperClass }) => {
    let wrapperedClass = "form-group";

    if (wrapperClass){
        wrapperedClass = `${wrapperedClass} ${wrapperClass}`
    }

    if (error && error.length > 0) {
        wrapperedClass += " has-error";
    }

    return (
        <div className={wrapperedClass}>
            {label && <label className="input-label" htmlFor={name}>{label}</label>}
            <div className="field">
                <input
                    id={id}
                    type={type}
                    name={name}
                    className="form-control"
                    placeholder={placeHolder}
                    value={value}
                    onChange={onChange}
                />
                {error&&<div className="alert alert-danger">{error}</div>}
            </div>
        </div>
    );
};

export default Input;