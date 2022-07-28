import React from 'react';
import {connect} from "react-redux";
import "./WooCommerceTable.css";
import {woocommerceStatus} from "../../../enums/enums";

function WooCommerceTable(props) {
    const plusReducer = (accumulator, curr) => accumulator + curr;
    return (
        <div className="woocommerce-table-wrapper">
            <table className="woocommerce-table">
                <thead>
                <tr className="woocommerce-table-header">
                    <th>
                        <input type="checkbox" id="allCheck"/>
                    </th>
                    <th>
                        SİPARİŞ BİLGİLERİ
                    </th>
                    <th>
                        NO
                    </th>
                    <th>
                        ALICI
                    </th>
                    <th>
                        BİLGİLER
                    </th>
                    <th>
                        ADET
                    </th>
                    <th>
                        BİRİM FİYAT
                    </th>
                    <th>
                        KARGO
                    </th>
                    <th>
                        FATURA
                    </th>
                    <th>
                        DURUM
                    </th>
                </tr>
                </thead>
                <tbody>
                {
                    props.list?.map(item => (
                        <>
                            <tr key={item.id} className="woocommerce-table-content">
                                <th>
                                    <input type="checkbox" id="allCheck"/>
                                </th>
                                <th>
                                    <div>
                                        <div>
                                            Woocommerce
                                        </div>
                                        <div>
                                            {new Date(item.item.date_created).toLocaleString("tr-TR")}
                                        </div>
                                        <div>
                                            {new Date(item.item.date_modified).toLocaleString("tr-TR")}
                                        </div>

                                    </div>

                                </th>
                                <th>
                                    {item.item.id}
                                </th>
                                <th className="woocommerce-table-item-name">
                                    {`${item.item.shipping.first_name} ${item.item.shipping.last_name}`}
                                </th>
                                <th>
                                    <div className="woocommerce-table-info-wrapper">
                                        <img className="woocommerce-table-image-item" src={item.image} alt=""/>
                                        {
                                            item.item.line_items.map((lineItem) => (
                                                <div>
                                                    <div>
                                                        {lineItem.name}
                                                    </div>
                                                    <div>
                                                        {`Stok Kodu: ${lineItem.sku}`}
                                                    </div>
                                                    <div>
                                                        {`Ürün Kodu: ${lineItem.product_id}`}
                                                    </div>
                                                </div>
                                            ))
                                        }
                                    </div>
                                </th>
                                <th>
                                    {item.item.line_items.map(line_item => line_item.quantity).reduce(plusReducer)}
                                </th>
                                <th>
                                    {
                                        item.item.line_items.map((lineItem) => (
                                            <div>
                                                <div>
                                                    {lineItem.price} TL
                                                </div>
                                            </div>
                                        ))
                                    }
                                </th>
                                <th>
                                    {
                                        item.item.shipping_lines.map((shipping_line) => (
                                            <div>
                                                <div>
                                                    {shipping_line.method_title}
                                                </div>
                                            </div>
                                        ))
                                    }
                                </th>
                                <th>
                                    {`Genel Toplam: ${item.item.total}`} TL
                                </th>
                                <th>
                                    {woocommerceStatus[item.item.status]}
                                </th>
                            </tr>
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

const mapStateToProps = ({woocommerce}) => {
    return {
        list: woocommerce.woocommerceOrderList,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(WooCommerceTable);
