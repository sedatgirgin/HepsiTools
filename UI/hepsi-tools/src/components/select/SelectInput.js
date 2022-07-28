import React from "react";

const SelectInput = ({
                         name,
                         label,
                         onChange,
                         defaultOption,
                         value,
                         error,
                         options,
                         wrapperClass
                     }) => {
    return (
        <div className={!!wrapperClass?`${wrapperClass}` : "form-group"}>
            {label && <label className="input-label" htmlFor={name}>{label}</label>}
            <select
                name={name}
                value={value}
                onChange={onChange}
                className="form-control"
            >
                <option value="">{defaultOption}</option>
                {options && options.map(option=>{
                    return (<option key={option.value} value = {option.value}>{option.text}</option>)
                })}
            </select>
            {error&&<div className="alert alert-danger">{error}</div>}
        </div>
    );
};

export default SelectInput;