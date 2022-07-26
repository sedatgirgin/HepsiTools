import React, {useEffect, useState} from 'react';
import "./CompetitionTable.css";
import RightCircleArrowIcon from "../../../static/assets/right-circle-arrow.png";
import DateIcon from "../../../static/assets/date.png";
import {getCompetitionList} from "../../../actions/competition";
import {connect} from "react-redux";

const status = {
    1: 'InProcess ',
    2: 'DecliningStock',
    3: 'OnHold',
    4: 'Cancel',
}

function CompetitionTable(props) {

    const [openedItemId, setOpenedItemId] = useState(-1);

    const setOpenedItem = (itemId) => {
        const initValue = -1;
        if (itemId === openedItemId) {
            setOpenedItemId(initValue);
        } else {
            setOpenedItemId(itemId);
        }

    }

    useEffect(() => {
        props.dispatchGetCompetitionList();
    }, []);

    return (
        <div className="competition-table-wrapper">
            <table className="competition-table">
                <thead>
                <tr className="competition-table-header">
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
                </thead>
                <tbody>
                {
                    props.list.map(item => (
                        <>
                            <tr key={item.id} className="competition-table-content">
                                <th>
                                    <input type="checkbox" id="allCheck"/>
                                </th>
                                <th>
                                    <img className={item.id === openedItemId ? "competition-table-opened-arrow" : ""}
                                         src={RightCircleArrowIcon} onClick={() => setOpenedItem(item.id)}
                                         alt="expanded"/>
                                </th>
                                <th>
                                    {item.id}
                                </th>
                                <th className="competition-table-item-name">
                                    {item.name}
                                </th>
                                <th>
                                    {status[item.statusType]}
                                </th>
                                <th>
                                    <img className="competition-table-item-date" src={DateIcon} alt="DateIcon"/>
                                    {new Date(item.updateDate).toLocaleString("tr-TR")}
                                </th>
                                <th>
                                    {item.repetitionCount}
                                </th>
                                <th>
                                    {`${new Date(item.startDate).toLocaleString("tr-TR")} > ${new Date(item.endDate).toLocaleString("tr-TR")}`}
                                </th>
                                <th>
                                    {item.salePrice} TL
                                </th>
                            </tr>
                            {
                                openedItemId === item.id &&
                                <tr>
                                    <th colSpan="9">
                                        {
                                            item.competitionAnalysesHistories ? item.competitionAnalysesHistories.map(competitionHistory => (
                                                <div>
                                                    competitionHistory
                                                </div>
                                            ))
                                                :
                                                <p className="competition-history-sub-title">
                                                    Geçmiş veri bulunmamaktadır.
                                                </p>
                                        }
                                    </th>
                                </tr>
                            }
                            <tr>

                            </tr>
                        </>

                    ))
                }
                </tbody>
            </table>
        </div>
    );
}

const mapStateToProps = ({competition}) => {
    return {
        list: competition.competitionList,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        dispatchGetCompetitionList: () => dispatch(getCompetitionList())
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CompetitionTable);
