import React from 'react';
import PlusIcon from "../../../../static/assets/plus-icon.png";
import StatusChanged from "../../../../static/assets/status-changed.png";
import PriceChanged from "../../../../static/assets/price-changed.png";
import "./History.css";

function History({history}) {


    if (history.historyType === 1) {
        return (
            <div className="history-item" key={history.id}>
                <div className="history-icon history-icon-plus">
                    <img src={PlusIcon} alt=""/>
                </div>
                <div className="history-title">
                    {history.note}
                </div>
                <div className="history-date">
                    {new Date(history.createDate).toLocaleString("tr-TR")}
                </div>
            </div>
        );
    } else if (history.historyType === 2) {
        return (
            <div className="history-item" key={history.id}>
                <div className="history-icon history-icon-status">
                    <img src={StatusChanged} alt=""/>
                </div>
                <div className="history-title">
                    {history.note}
                </div>
            </div>
        )
    }else {
        return (
            <div className="history-item" key={history.id}>
                <div className="history-icon history-icon-price">
                    <img src={PriceChanged} alt=""/>
                </div>
                <div className="history-title">
                    {history.note}
                </div>
            </div>
        )
    }

}

export default History;