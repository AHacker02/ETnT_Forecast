import React, {useRef, useState} from 'react';
import './App.css';
import Worksheet from "./features/worksheet/worksheet";
import {useDispatch} from "react-redux";
import {uploadForecast} from "./features/worksheet/worksheetSlice";

function App() {
    const ref = useRef();
    const dispatch = useDispatch();
    const onFileChange = event => {
        debugger;
        if(event.target.files[0]) {
            dispatch(uploadForecast(event.target.files[0]));
        }
    };
    return (
        <div className="App">
            <div className="ui top fixed inverted menu">
                <div className="ui container no-left-margin">
                    <a className="header item" href="#root">
                        ET & T
                    </a>
                </div>
                <div className="ui no-right-margin">
                    <i className="download icon large" onClick={()=>{ref.current.onBtExport()}}></i>
                </div>
                <div className="ui no-right-margin">
                    <i className="upload icon large" onClick={()=>document.getElementById("upload").click()}></i>
                    <input id="upload" type="file" onChange={onFileChange} hidden/>
                </div>
            </div>
            <div className="ui main text container">
                <Worksheet ref={ref} />
            </div>
        </div>
);
}

export default App;
