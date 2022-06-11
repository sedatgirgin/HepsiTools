import React from 'react';
import "./Panel.css";
import Header from "../components/header/Header";
import Sidebar from "../components/sidebar/Sidebar";
import Rekabet from "./rekabet/Rekabet";

function Panel(props) {
    return (
        <div className="panel">
            <Sidebar/>
            <Header/>
            <div className="panel-body">
                <Rekabet/>
            </div>
        </div>

    );
}

export default Panel;