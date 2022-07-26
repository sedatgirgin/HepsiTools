import React from 'react';
import "./Header.css";
import NotificationIcon from '../../static/assets/Notification icon.png'
import HelpIcon from '../../static/assets/help.png'
import ProfileIcon from '../../static/assets/Rectangle 1650.png'
import {connect} from "react-redux";
function Header(props) {
    return (
        <div className="header">
            <div className="header-title-wrapper">
                <div className="header-title">
                    Rekabet
                </div>
                <div className="header-count">
                    {props.competitionCount}
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

const mapStateToProps = ({competition}) => {
    return {
        competitionCount: competition.competitionList ? competition.competitionList.length : '',
    };
};


export default connect(mapStateToProps, null)(Header);
