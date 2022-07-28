import React, {useEffect, useState} from 'react';
import "./CompetitionTable.css";
import RightCircleArrowIcon from "../../../static/assets/right-circle-arrow.png";
import PlusIcon from "../../../static/assets/plus-icon.png";
import DateIcon from "../../../static/assets/date.png";
import {getCompetitionList} from "../../../actions/competition";
import {status} from "../../../enums/enums";
import {connect} from "react-redux";
import {client} from "../../../api/client";
import History from "./history/History";


function CompetitionTable(props) {

    function getStatusClass(status) {
        switch (status) {
            case 1:
                return 'competition-table-item-status-progress'
            case 2:
                return 'competition-table-item-status-declining-stock'
            case 3:
                return 'competition-table-item-status-onhold'
            case 4:
                return 'competition-table-item-status-cancel'
            default:
                return '';
        }
    }

    const [openedItemId, setOpenedItemId] = useState(-1);
    const [openedHistory, setOpenedHistory] = useState([]);

    const setOpenedItem = (itemId) => {
        const initValue = -1;

        if (itemId !== initValue) {
            client.post(`/Competion/GetAnalsesHistory?competitionId=${itemId}`)
                .then(value => {
                    setOpenedHistory(value.data.reverse());
                    if (itemId === openedItemId) {
                        setOpenedItemId(initValue);
                    } else {
                        setOpenedItemId(itemId);
                    }
                })
                .catch(reason => setOpenedHistory(undefined));
        }


    }

    useEffect(() => {
        props.dispatchGetCompetitionList();
    }, [props.competitionList]);

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
                                    {
                                        <div className="competition-table-item-wrapper">
                                            <div className={`competition-table-item-status ${getStatusClass(item.statusType)}`}>
                                                <div className="competition-table-item-status-circle"/>
                                                {status[item.statusType]}
                                            </div>
                                        </div>
                                    }
                                </th>
                                <th>
                                    <img className="competition-table-item-date" src={DateIcon} alt="DateIcon"/>
                                    {new Date(item.updateDate).toLocaleString("tr-TR")}
                                </th>
                                <th>
                                    <div className="competition-table-item-wrapper">
                                        <div className="competition-table-item-repetition-count">
                                            {!!item.repetitionCount ?
                                                item.repetitionCount :
                                                <img src={PlusIcon} alt=""/>
                                            }
                                        </div>
                                    </div>
                                </th>
                                <th>
                                    <div className="competition-table-item-wrapper">
                                        <div className="competition-table-date-wrapper">{new Date(item.startDate).toLocaleString("tr-TR")}</div>>
                                        <div className="competition-table-date-wrapper">{new Date(item.endDate).toLocaleString("tr-TR")}</div>
                                    </div>

                                </th>
                                <th>
                                    {item.salePrice} TL
                                </th>
                            </tr>
                            {
                                openedItemId === item.id &&
                                <tr>
                                    <th colSpan="9">
                                        <div className="competition-history-row">


                                            {
                                                !!openedHistory && openedHistory.length > 0 ?
                                                    <div>
                                                        {openedHistory.map((history, index) => (
                                                            <>
                                                                {
                                                                    0 !== index &&
                                                                    <div className="competition-history-space"/>
                                                                }
                                                                <History history={history}/>
                                                            </>
                                                        ))}
                                                    </div>
                                                    :
                                                    <p className="competition-history-sub-title">
                                                        Geçmiş veri bulunmamaktadır.
                                                    </p>
                                            }
                                        </div>
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
