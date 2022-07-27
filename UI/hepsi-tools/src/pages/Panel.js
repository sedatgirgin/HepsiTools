import React from 'react';
import "./Panel.css";
import Header from "../components/header/Header";
import Sidebar from "../components/sidebar/Sidebar";
import {Redirect, Route, Switch, withRouter} from "react-router-dom";
import Competition from "./rekabet/Competition";
import WooCommerce from "./wooCommerce/WooCommerce";

function Panel(props) {

    return (
        <div className="panel">
            <Sidebar/>
            <Header/>
            <div className="panel-body">
                <Switch>
                    <Route path='/panel/competition'><Competition/></Route>
                    <Route path='/panel/woocommerce'><WooCommerce/></Route>
                    <Redirect from='/panel' to='/panel/competition'/>
                </Switch>
            </div>

        </div>

    );
}


export default Panel;