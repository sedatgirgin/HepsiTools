import {GET_WOOCOMMERCE_LIST, GET_WOOCOMMERCE_ORDER_LIST} from "../actions/actionTypes";

const initialState = {
    woocommerceList:[],
    woocommerceId:"",
    woocommerceOrderList:[]
};

export default function woocommerce(state = initialState, action) {
    switch (action.type) {
        case GET_WOOCOMMERCE_LIST:
            return {
                woocommerceList: action.payload,
                woocommerceId: "",
                woocommerceOrderList: []
            };
        case GET_WOOCOMMERCE_ORDER_LIST:
            return {
                woocommerceList: state.woocommerceList,
                woocommerceId: action.woocommerceId,
                woocommerceOrderList: action.payload
            };
        default:
            return state;
    }
}