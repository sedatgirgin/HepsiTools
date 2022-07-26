import {GET_PRODUCT_LIST, GET_COMPANY_LIST, GET_COMPETITION_LIST} from "./actionTypes";
import {client} from "../api/client";

export const getCompanyList = () => {
    return (dispatch) => {
        return client.get('/Competion/GetCompanyList')
            .then((res) => {
                console.log(res)
                dispatch({type: GET_COMPANY_LIST, payload: res.data});
            });
    };
};

export const getProductList = (companyId) => {
    return (dispatch) => {
        return client.get(`/Competion/GetTrendYolProductList?companyId=${companyId}`)
            .then((res) => {
                dispatch({type: GET_PRODUCT_LIST, payload: res.data?.content, selectCompanyId: companyId});
            });
    };
};

export const getCompetitionList = () => {
    return (dispatch) => {
        return client.get('/Competion/GetCompetitionsByUser')
            .then((res) => {
                dispatch({type: GET_COMPETITION_LIST, payload: res?.data[0]?.competitionAnalyses});
            });
    }
}

