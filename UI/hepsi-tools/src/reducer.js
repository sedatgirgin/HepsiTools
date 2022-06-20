import authorization from './reducer/authorizationReducers';
import {combineReducers} from 'redux';
const reducer = combineReducers({
    authorization,
})

export default reducer;