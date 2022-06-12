import React from 'react';
import "./CompotitionTable.css";
import RightCircleArrowIcon from "../../../static/assets/right-circle-arrow.png";

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
                <tr>
                    <th>
                        <input type="checkbox" id="allCheck"/>
                    </th>
                    <th>
                        <img src={RightCircleArrowIcon} alt="expanded"/>
                    </th>
                    <th>
                        1
                    </th>
                    <th>
                        Allosaurus web app
                    </th>
                    <th>
                        İşlemde
                    </th>
                    <th>
                        15 Mar 2021, 12:47 PM
                    </th>
                    <th>
                        3
                    </th>
                    <th>
                        15 May 2021 > 15 Aug 2021
                    </th>
                    <th>
                        100.90 TL
                    </th>
                </tr>
            </table>
        </div>
    );
}

export default CompotitionTable;