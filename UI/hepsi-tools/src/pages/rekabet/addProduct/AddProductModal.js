import React, {useState} from 'react';
import {connect} from "react-redux";
import {getCompetitionList, getProductList} from "../../../actions/competition";
import DateTimePicker from "react-datetime-picker";
import Input from "../../../components/input/Input";
import "./AddProductModal.css";
import {client} from "../../../api/client";
import SelectInput from "../../../components/select/SelectInput";

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

    const [errors, setErrors] = useState({});

    const onCompanySelected = (e) => {
        props.dispatchGetProductList(e.target.value);
    }

    const onChange = (e) => {
        const {name, value} = e.target;
        setCompetition((prevCompetition) => ({...prevCompetition, [name]: value}));
        setErrors((prevErrors => ({...prevErrors, [name]: undefined})))
    }

    const onChageStartDateTime = (dateTime) => {
        setCompetition((prevCompetition) => ({...prevCompetition, startDate: dateTime}));
        setErrors((prevErrors => ({...prevErrors, startDate: undefined})))
    }

    const onChageEndDateTime = (dateTime) => {
        setCompetition((prevCompetition) => ({...prevCompetition, endDate: dateTime}));
        setErrors((prevErrors => ({...prevErrors, endDate: undefined})))
    }

    function validate() {
        let errors = {};
        if (!competition.parserLink) {
            errors = {parserLink: "Ürünün linkini giriniz."}
        }

        if (!props.selectCompanyId) {
            errors = {...errors, selectCompanyId: "Şirket Seçiniz."}
        }

        if (!competition.product) {
            errors = {...errors, product: "Şirket Seçiniz."}
        }

        if (errors) {
            setErrors(errors);
        }

        return !!errors;
    }

    const onClose = () => {
        props.dispatchGetCompetitionList()
        props.dispatchGetProductList("")
        props.onClose();
    }

    const oncompetitionAdd = () => {
        if (validate()) {
            client.post('/Competion/AddCompetition', {
                "name": competition.name,
                "highestPrice": competition.highestPrice,
                "lowestPrice": competition.lowestPrice,
                "multiple": competition.multiple,
                "startDate": competition.startDate,
                "endDate": competition.endDate,
                "product": competition.product,
                "parserLink": competition.parserLink,
                "productLink": competition.name,
                "productInfo": competition.name,
                "note": competition.name,
                "statusType": competition.statusType,
                "companyId": props.selectCompanyId
            })
                .then((res) => {
                    props.onClose();
                });
        }
    }

    return (
        <div className="add-product-modal-wrapper">
            <h5 className="add-product-title">
                Yeni Rekabet Ekle
            </h5>
            <div className="add-product-modal-input-container">
                <Input
                    type="text"
                    name="parserLink"
                    id="parserLink"
                    wrapperClass="competition-input-wrapper"
                    value={competition.parserLink}
                    label={"Ürün URL"}
                    error={errors.parserLink}
                    onChange={onChange}/>
                <SelectInput
                    name="selectCompanyId"
                    label="Şirket Listesi"
                    wrapperClass="competition-input-wrapper"
                    value={props.selectCompanyId || ""}
                    defaultOption="Seçim yapınız"
                    options={props.companyList.map((category) => ({
                        value: category.id,
                        text: category.customResourceName,
                    }))}
                    onChange={onCompanySelected}
                    error={errors.selectCompanyId}
                />
                {
                    !!props.selectCompanyId &&
                    <div>
                        <SelectInput
                            name="product"
                            label="Ürün Listesi"
                            value={competition.product || ""}
                            defaultOption="Seçim yapınız"
                            options={props.productList.map((category) => ({
                                value: category.id,
                                text: category.title,
                            }))}
                            onChange={onChange}
                            error={errors.product}
                        />
                    </div>

                }
                {
                    !!competition.product &&
                    <div>
                        <Input
                            type="text"
                            name="name"
                            id="competition_name"
                            wrapperClass="competition-input-wrapper"
                            value={competition.name}
                            label={"Rekabet Adı"}
                            error={errors.name}
                            onChange={onChange}/>
                        <div className={"competition-product-price-wrapper"}>
                            <Input
                                type="number"
                                name="highestPrice"
                                id="high_price"
                                wrapperClass="competition-input-wrapper"
                                value={competition.highestPrice}
                                error={errors.highestPrice}
                                label={"Min.Fiyat (Kdv Dahil)"}
                                onChange={onChange}/>
                            <Input
                                type="number"
                                name="lowestPrice"
                                id="low_price"
                                wrapperClass="competition-input-wrapper"
                                value={competition.lowestPrice}
                                error={errors.lowestPrice}
                                label={"Max.Fiyat (Kdv Dahil)"}
                                onChange={onChange}/>
                        </div>
                        <Input
                            type="number"
                            name="multiple"
                            id="multiple"
                            wrapperClass="competition-input-wrapper"
                            value={competition.multiple}
                            error={errors.multiple}
                            label={"Fiyat Kademesi"}
                            onChange={onChange}/>
                        <div className={"competition-product-price-wrapper"}>
                            <div className="competition-input-wrapper">
                                <label htmlFor="startDate" className="input-label">Rekabet Başlangıç Tarihi</label>
                                <DateTimePicker
                                    className="date-time-picker form-control"
                                    name="startDate"
                                    value={competition.startDate}
                                    minDate={new Date(Date.now())}
                                    onChange={onChageStartDateTime}/>
                                {errors.startDate && <div className="alert alert-danger">{errors.startDate}</div>}
                            </div>
                            <div className="competition-input-wrapper">
                                <label htmlFor="endDate" className="input-label">Rekabet Bitiş Tarihi</label>
                                <DateTimePicker
                                    className="date-time-picker form-control"
                                    name="endDate"
                                    minDate={new Date(Date.now())}
                                    value={competition.endDate}
                                    onChange={onChageEndDateTime}/>
                                {errors.endDate && <div className="alert alert-danger">{errors.endDate}</div>}
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
        dispatchGetProductList: (companyId) => dispatch(getProductList(companyId)),
        dispatchGetCompetitionList: () => dispatch(getCompetitionList())
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(AddProductModal);