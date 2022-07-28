import authorization from './reducer/authorizationReducers';
import {combineReducers} from 'redux';
import competition from "./reducer/competitionReducer";
import woocommerce from "./reducer/woocommerceReducer";
const reducer = combineReducers({
    authorization,
    competition,
    woocommerce
})

export default reducer;