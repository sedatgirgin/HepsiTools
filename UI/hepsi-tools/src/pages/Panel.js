import React, {useEffect, useState} from 'react';
import "./Panel.css";
import Header from "../components/header/Header";
import Sidebar from "../components/sidebar/Sidebar";
import Compotition from "./rekabet/Compotition";
import {client} from "../api/client";

function Panel(props) {

    const [state, setState] = useState({
        list:[]
    });

    useEffect ( ()=> {
            client.get('/Competion/GetCompetitionsByUser')
                .then((res) => {
                    console.log(res.data[0].competitionAnalyses);
                    setState({list: res.data[0].competitionAnalyses});
                });


        // tarayıcının başlık bölümünü değiştirmemizi sağlar
        // document.title = `You clicked ${count} times`;
    }, []);

    return (
        <div className="panel">
            <Sidebar/>
            <Header/>
            <div className="panel-body">
                <Compotition list={state.list}/>
            </div>
        </div>

    );
}

export default Panel;