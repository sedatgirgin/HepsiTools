import React from 'react';
import "./Panel.css";
import Header from "../components/header/Header";
import Sidebar from "../components/sidebar/Sidebar";
import Compotition from "./rekabet/Compotition";

function Panel(props) {
    return (
        <div className="panel">
            <Sidebar/>
            <Header/>
            <div className="panel-body">
                <Compotition/>
            </div>
        </div>

    );
}

export default Panel;