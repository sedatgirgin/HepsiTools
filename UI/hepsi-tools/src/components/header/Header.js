import React from 'react';
import "./Header.css";
import NotificationIcon from '../../static/assets/Notification icon.png'
import HelpIcon from '../../static/assets/help.png'
import ProfileIcon from '../../static/assets/Rectangle 1650.png'
function Header(props) {
    return (
        <div className="header">
            <div className="header-title-wrapper">
                <div className="header-title">
                    Rekabet
                </div>
                <div className="header-count">
                    27
                </div>
            </div>
            <div className="header-actions">
                <div className="header-actions-icon">
                    <img src={NotificationIcon} alt="notification icon"/>
                </div>
                <div className="header-actions-icon">
                    <img src={HelpIcon} alt="help icon"/>
                </div>
                <div className="header-actions-icon">
                    <img src={ProfileIcon} alt="profile icon"/>
                </div>
            </div>

        </div>
    );
}

export default Header;