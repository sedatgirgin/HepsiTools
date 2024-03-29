import React, {useState} from 'react';
import "./Competition.css";
import TableIcon from "../../static/assets/table-icon.png";
import ReactModal from 'react-modal';
import CompetitionTable from "./table/CompetitionTable";
import AddProductModal from "./addProduct/AddProductModal";
import {getCompanyList} from "../../actions/competition";
import {connect} from "react-redux";
import SelectInput from "../../components/select/SelectInput";
import Input from "../../components/input/Input";

function Competition(props) {
    const [isAddProductModalOpen, setAddProductModalOpen] = useState(false);

    const onAddProduct = () => {
        props.dispatchGetCompanyList();
        setAddProductModalOpen(true);
    }

    const onAddProductModalClose = () => {
      setAddProductModalOpen(false);
    }
    return (
        <div className="competition">
            <div className="competition-action-wrapper">
                <div className="competition-filter-action">
                    <button className="secondary-btn competition-action-item">
                        <img src={TableIcon} alt="Ürün kontrol et"/>
                    </button>
                    <SelectInput
                        options={[
                            {value:'all', text:'Hepsi'},
                            {value:'name', text:'Ürün Adı'},
                            {value:'status', text:'Durum'},
                        ]}
                        wrapperClass="competition-action-item"
                        defaultOption={'Filtre'}
                    />
                    <Input
                        type="text"
                        wrapperClass="competition-action-item"
                        placeHolder="Arama Yap..."
                    />
                </div>
                <div>
                    <button
                        className="primary-btn competition-action-item"
                        onClick={onAddProduct}
                    >
                        + Rekabet için ürün ekle
                    </button>
                </div>

            </div>
            <div>
                <CompetitionTable/>
                <ReactModal
                    shouldCloseOnEsc = {true}
                    shouldCloseOnOverlayClick = {true}
                    onRequestClose={onAddProductModalClose}
                    isOpen={isAddProductModalOpen}
                    className="competition-react-modal">

                    <AddProductModal onClose={onAddProductModalClose}/>
                </ReactModal>
            </div>
        </div>
    );
}

const mapDispatchToProps = (dispatch) => {
    return {
        dispatchGetCompanyList: () => dispatch(getCompanyList())
    };
};

export default connect(null, mapDispatchToProps)(Competition);
