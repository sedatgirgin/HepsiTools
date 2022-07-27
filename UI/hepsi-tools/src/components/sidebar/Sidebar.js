import React from 'react';
import "./Sidebar.css";
import RekabetLogo from '../../static/assets/active.png';
import SettingsLogo from '../../static/assets/Subtract.png'
import Logo from '../../static/assets/Logo.png';
import {Link} from "react-router-dom";

function Sidebar(props) {

    return (
        <div className="sidebar">
            <div className="sidebar-logo">
                <img src={Logo} alt="Logo"/>
            </div>
            <div className="sidebar-menu-item">
                <Link to="/panel/competition">
                    <img src={RekabetLogo} alt="Menu item"/>
                </Link>

            </div>
            <div className="sidebar-menu-item">
                <Link to="/panel/woocommerce">
                    <img src={RekabetLogo} alt="Menu item"/>
                </Link>

            </div>
            <div className="sidebar-menu-item">
                <img src={SettingsLogo} alt="Menu item"/>
            </div>
        </div>
    );
}



export default Sidebar;