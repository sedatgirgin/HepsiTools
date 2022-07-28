import React, {useEffect, useState} from 'react';
import TableIcon from "../../static/assets/table-icon.png";
import SelectInput from "../../components/select/SelectInput";
import {getWoocommerceList, getWoocommerceOrderList} from "../../actions/woocommerce";
import {connect} from "react-redux";
import "./WooCommerce.css";
import WooCommerceTable from "./table/WooCommerceTable";

function WooCommerce(props) {

    useEffect(() => {
        props.dispatchGetWooCommerceList();
    }, [props.woocommerceList.length]);

    const onChangeWoocommerce = (e) => {
        const {value} = e.target;
        props.dispatchGetWooCommerceOrderList(value);
    }

    return (
        <div>
            <div className="woocommerce-action-wrapper">
                <div className="woocommerce-filter-action">
                    <button className="secondary-btn woocommerce-action-item">
                        <img src={TableIcon} alt="Ürün kontrol et"/>
                    </button>
                    <select className="woocommerce-filter-select" name="filterSelect" id="filterSelect">
                        Hepsi
                    </select>
                    <SelectInput
                        options={props.woocommerceList?.map((woocommerce) => ({
                            value: woocommerce.id,
                            text: woocommerce.name,
                        }))}
                        defaultOption={"Firma Seçiniz"}
                        value={props.wooCommerceId}
                        onChange={onChangeWoocommerce}
                        wrapperClass="woocommerce-action-item"
                    />
                </div>
                <div>
                </div>

            </div>
            <div>
                <WooCommerceTable/>
            </div>
        </div>
    );
}

const mapStateToProps = ({woocommerce}) => {
    console.log("woocommerce", woocommerce);
    return {
        woocommerceList: woocommerce.woocommerceList,
        woocommerceId: woocommerce.woocommerceId
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        dispatchGetWooCommerceList: () => dispatch(getWoocommerceList()),
        dispatchGetWooCommerceOrderList: (wooCommerceId) => dispatch(getWoocommerceOrderList(wooCommerceId))
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(WooCommerce);
