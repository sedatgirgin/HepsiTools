import {GET_COMPANY_LIST, GET_PRODUCT_LIST, ADD_competition, GET_COMPETITION_LIST} from "../actions/actionTypes";

const initialState = {
    companyList:[],
    selectCompanyId:undefined,
    productList: [],
    competitionList: []
};

export default function competition(state = initialState, action) {
    switch (action.type) {
        case GET_COMPETITION_LIST:
            return {
                ...state,
                competitionList: action.payload
            };
        case GET_COMPANY_LIST:
            return {
                competitionList: state.companyList,
                selectCompanyId: undefined,
                companyList: action.payload,
                productList: []
            };
        case GET_PRODUCT_LIST:
            return {
                ...state,
                selectCompanyId: action.selectCompanyId,
                productList: action.payload,
            };
        case ADD_competition:
            return {
                authChecked: true,
                loggedIn: true,
                productList: action.payload,
            };
        default:
            return state;
    }
}