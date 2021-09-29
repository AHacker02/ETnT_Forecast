import React, {useEffect, useRef, useState} from 'react';
import './App.css';
import Worksheet from "./features/worksheet/worksheet";
import {useDispatch, useSelector} from "react-redux";
import {
    getForecast,
    getLookupData,
    saveForecast,
    selectFyYears, selectSelectedYear, setSelectedYear,
    uploadForecast
} from "./features/worksheet/worksheetSlice";
import {Dropdown} from "semantic-ui-react";

function App() {
    const worksheetRef = useRef();
    const dispatch = useDispatch();
    const [isDirty, setIsDirty] = useState(false);
    const fyYears = useSelector(selectFyYears);
    const selectedYear = useSelector(selectSelectedYear)

    const onFileChange = event => {
        if (event.target.files[0]) {
            dispatch(uploadForecast(event.target.files[0]));
        }
    };

    const handleItemClick = (_e, x) => {
        dispatch(setSelectedYear(x.value));
    };

    useEffect(() => {
        dispatch(getLookupData())
    }, [])

    useEffect(() => {
        if (selectedYear) {
            dispatch(getForecast(selectedYear));
        }
    }, [selectedYear])

    // useEffect(()=>{
    //     console.log(isDirty);
    //     if(isDirty){
    //         dispatch(saveForecast());
    //     }
    // },[isDirty])

    return (
        <div className="App">
            <div className="ui top fixed inverted menu">
                <div className="ui container no-left-margin">
                    <a className="header strong item" href="#">
                        ET & T
                    </a>
                </div>
                <Dropdown
                    className="ui item link"
                    name="Year"
                    placeholder="Select Year"
                    pointing
                    value={selectedYear}
                    text={selectedYear}
                >
                    <Dropdown.Menu>
                        {
                            fyYears.map(year => (
                                <Dropdown.Item
                                    value={year}
                                    text={year}
                                    onClick={handleItemClick}
                                />
                            ))
                        }
                    </Dropdown.Menu>
                </Dropdown>
                <div className="ui item">
                    <i className="save icon large" onClick={() => {
                        dispatch(saveForecast())
                    }}></i>
                </div>
                <div className="ui item">
                    <i className="download icon large no-left-margin" onClick={() => {
                        worksheetRef.current.onBtExport()
                    }}></i>
                </div>
                <div className="ui item">
                    <i className="upload icon large" onClick={() => document.getElementById("upload").click()}></i>
                    <input id="upload" type="file" onChange={onFileChange} hidden/>
                </div>
            </div>
            <div className="ui main text container">
                <Worksheet ref={worksheetRef} setIsDirty={setIsDirty}/>
            </div>
        </div>
    );
}

export default App;
