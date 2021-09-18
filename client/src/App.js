import React from 'react';
import './App.css';
import Worksheet from "./features/worksheet/worksheet";

function App() {
    return (
        <div className="App">
            <div className="ui top fixed inverted menu">
                <div className="ui container no-left-margin">
                    <a className="header item" href="#root">
                        ET & T
                    </a>
                </div>
            </div>
            <div className="ui option bar">
HIIIIIIIIIIIIIII
            </div>
            <div className="ui main text container">
                <Worksheet/>
            </div>
        </div>
);
}

export default App;
