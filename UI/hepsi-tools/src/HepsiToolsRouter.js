import React from "react";
import {
    Switch,
    Route, withRouter, BrowserRouter, Redirect,
} from "react-router-dom";
import {wrappedComponents} from './pages/wrapped/wrappedComponents'
import Panel from "./pages/Panel";


const HepsiToolsRouter = () => (
    <Switch>
        <Route exact path="/login"
               render={(routerProps) => <wrappedComponents.AuthenticatedLogin {...routerProps}/>}/>
        <Route exact path="/register"
               render={(routerProps) => <wrappedComponents.AuthenticatedRegister {...routerProps}/>}/>
        <Route path='/panel'
               render={(routerProps) => <wrappedComponents.ProtectedPanel {...routerProps} protected/>}></Route>
        <Redirect exact from='/' to='/panel'/>
    </Switch>
);
export default HepsiToolsRouter
