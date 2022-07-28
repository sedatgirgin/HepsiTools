import {client} from "../api/client";
import {GET_WOOCOMMERCE_LIST, GET_WOOCOMMERCE_ORDER_LIST} from "./actionTypes";

export const getWoocommerceList = () => {
    return (dispatch) => {
        return client.get('/WooCommerce/GetList')
            .then((res) => {
                dispatch({type: GET_WOOCOMMERCE_LIST, payload: res?.data});
            });
    }
}

export const getWoocommerceOrderList = (wooCommerceId) => {
    return (dispatch) => {
        return client.post(`/WooCommerce/GetOrders?wooCommerceId=${wooCommerceId}`)
            .then((res) => {
                dispatch({type: GET_WOOCOMMERCE_ORDER_LIST, payload: res?.data?.orders, wooCommerceId:wooCommerceId});
            });
    }
}