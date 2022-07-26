import React, {useState} from 'react';
import {connect} from "react-redux";
import {getProductList} from "../../../actions/competition";
import DateTimePicker from "react-datetime-picker";
import Input from "../../../components/input/Input";
import "./AddProductModal.css";

function AddProductModal(props) {

    const [competition, setCompetition] = useState({
            product: "",
            name: "",
            highestPrice: 0,
            lowestPrice: 0,
            multiple: 0,
            startDate: new Date(Date.now()),
            endDate: new Date(Date.now()),
            parserLink: "",
            statusType: 1
        }
    );

    const onCompanySelected = (e) => {
        props.dispatchGetProductList(e.target.value);
    }
    const onProductSelected = (e) => {
        setCompetition({...competition, product: e.target.value})
    }
    const onChange = (e) => {
        const {name, value} = e.target;
        setCompetition({...competition, [name]: value})
    }

    const onClose = () => {
      props.onClose();
    }

    const oncompetitionAdd = () => {

    }

    return (
        <div className="add-product-modal-wrapper">
            <h5 className="add-product-title">
                Yeni Rekabet Ekle
            </h5>
            <div className="add-product-modal-input-container">
                <label htmlFor="company_list" className="input-label">Şirket Listesi</label>
                <select
                    className="form-control"
                    name="company_list" id="company_list"
                    onChange={onCompanySelected}
                    value={props.selectCompanyId}>
                    <option value={null}>Seçiniz</option>
                    {props.companyList?.map((option) => (
                        <option value={option.id}>{option.customResourceName}</option>
                    ))}
                </select>
                {
                    !!props.selectCompanyId &&
                    <div>
                        <label htmlFor="product_list" className="input-label">Ürün Listesi</label>
                        <select className="form-control"
                                name="product_list"
                                id="product_list"
                                onChange={onProductSelected}
                                value={competition.product}>
                            {props.productList?.map((option) => (
                                <option value={option.id}>{option.title}</option>
                            ))}
                        </select>
                    </div>

                }
                {
                    !!competition.product &&
                    <div>
                        <Input
                            type="text"
                            name="name"
                            id="competition_name"
                            value={competition.name}
                            label={"Rekabet Adı"}
                            onChange={onChange}/>
                        <div className={"competition-product-price-wrapper"}>
                            <Input
                                type="number"
                                name="highestPrice"
                                id="high_price"
                                wrapperClass="competition-input-wrapper"
                                value={competition.highestPrice}
                                label={"Düşük Fiyat"}
                                onChange={onChange}/>
                            <Input
                                type="number"
                                name="lowestPrice"
                                id="low_price"
                                wrapperClass="competition-input-wrapper"
                                value={competition.lowestPrice}
                                label={"Yüksek Fiyat"}
                                onChange={onChange}/>
                        </div>
                        <Input
                            type="number"
                            name="multiple"
                            id="multiple"
                            value={competition.multiple}
                            label={"Kat Sayı"}
                            onChange={onChange}/>
                        <div className={"competition-product-price-wrapper"}>
                            <div className="competition-datetime-input-wrapper">
                                <label htmlFor="startDate" className="input-label">Başlangıç Tarihi</label>
                                <DateTimePicker
                                    className="date-time-picker form-control"
                                    name="startDate"
                                    value={competition.startDate}
                                    onChange={onChange}/>
                            </div>
                            <div className="competition-datetime-input-wrapper">
                                <label htmlFor="endDate" className="input-label">Bitiş Tarihi</label>
                                <DateTimePicker
                                    className="date-time-picker form-control"
                                    name="endDate"
                                    value={competition.endDate}
                                    onChange={onChange}/>
                            </div>
                        </div>
                    </div>
                }
            </div>
            <div className="add-product-modal-btn-container">
                <button className="secondary-btn add-product-modal-btn-left" onClick={onClose}>İptal</button>
                <button className="primary-btn" onClick={oncompetitionAdd}>Rekabet Ekle</button>
            </div>

        </div>

    );
}

const mapStateToProps = ({competition}) => {
    console.log('addProductState', competition);
    return {
        companyList: competition.companyList,
        productList: competition.productList,
        selectCompanyId: competition.selectCompanyId,
        selectProductId: competition.selectProductId
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        dispatchGetProductList: (companyId) => dispatch(getProductList(companyId))
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(AddProductModal);