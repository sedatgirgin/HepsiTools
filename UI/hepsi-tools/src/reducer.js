import authorization from './reducer/authorizationReducers';
import {combineReducers} from 'redux';
import competition from "./reducer/competitionReducer";
const reducer = combineReducers({
    authorization,
    competition,
})

export default reducer;