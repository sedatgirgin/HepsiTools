import React from 'react';
import "./CompotitionTable.css";
import RightCircleArrowIcon from "../../../static/assets/right-circle-arrow.png";
import DateIcon from "../../../static/assets/date.png";

const status = {
    1:'InProcess ',
    2:'DecliningStock',
    3:'OnHold',
    4:'Cancel',
}

function CompotitionTable(props) {
    return (
        <div className="compotition-table-wrapper">
            <table className="compotition-table">
                <tr className="compotition-table-header">
                    <th>
                        <input type="checkbox" id="allCheck"/>
                    </th>
                    <th>

                    </th>
                    <th>
                        #
                    </th>
                    <th>
                        ÜRÜN ADI
                    </th>
                    <th>
                        DURUMU
                    </th>
                    <th>
                        GÜNCELLEME TARİHİ
                    </th>
                    <th>
                        TEKRARLAMA
                    </th>
                    <th>
                        REKABET TARİHİ
                    </th>
                    <th>
                        SATIŞ FİYATI
                    </th>
                </tr>
                {
                    props.list.map(item => (
                        <tr className="compotition-table-content">
                            <th>
                                <input type="checkbox" id="allCheck"/>
                            </th>
                            <th>
                                <img src={RightCircleArrowIcon} alt="expanded"/>
                            </th>
                            <th>
                                {item.id}
                            </th>
                            <th className="compotition-table-item-name">
                                {item.name}
                            </th>
                            <th>
                                {status[item.statusType]}
                            </th>
                            <th>
                                <img className="compotition-table-item-date" src={DateIcon} alt="DateIcon"/>
                                {new Date(item.updateDate).toLocaleString("tr-TR")}
                            </th>
                            <th>
                                {item.repetitionCount}
                            </th>
                            <th>
                                {`${new Date(item.startDate).toLocaleString("tr-TR")} > ${new Date(item.endDate).toLocaleString("tr-TR")}` }
                            </th>
                            <th>
                                {item.salePrice} TL
                            </th>
                        </tr>
                    ))
                }
            </table>
        </div>
    );
}



export default CompotitionTable;