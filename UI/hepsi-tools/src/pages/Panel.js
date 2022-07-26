import React, {useEffect, useState} from 'react';
import "./Panel.css";
import Header from "../components/header/Header";
import Sidebar from "../components/sidebar/Sidebar";
import Competition from "./rekabet/Competition";

function Panel(props) {

    return (
        <div className="panel">
            <Sidebar/>
            <Header/>
            <div className="panel-body">
                <Competition/>
            </div>
        </div>

    );
}


export default Panel;