import React from 'react';
import "./Compotition.css";
import TableIcon from "../../static/assets/table-icon.png"
import CompotitionTable from "./table/CompotitionTable";
function Compotition(props) {
    return (
        <div className="compotition">
            <div className="compotition-action-wrapper">
                <div className="compotition-filter-action">
                    <button className="secondary-btn compotition-action-item">
                        <img src={TableIcon} alt="Ürün kontrol et"/>
                    </button>
                    <select className="compotition-filter-select" name="filterSelect" id="filterSelect">
                        Hepsi
                    </select>
                </div>
                <div>
                    <button className="primary-btn compotition-action-item">+ Ürün ekle</button>
                </div>

            </div>
            <div>
                <CompotitionTable list={props.list}/>
            </div>
        </div>
    );
}

export default Compotition;