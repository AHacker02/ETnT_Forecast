import React from 'react';
import {AgGridReact} from "ag-grid-react";
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-material.css';
import './worksheet.css';

const Worksheet = () => {
    const data = [
        {
            "etNtOrg": "Aviation Biz Ops(BGS)",
            "manager": "Manager Name",
            "usFocal": "US F Name",
            "project": "Proj Name",
            "skillGroup": "Java FullStack",
            "businessUnit": "BGS",
            "capabilities": "Training Solutions",
            "chargeLine": "",
            "forecastConfidence": "Soft Commitment",
            "comments": "Java Full Stack",
            "jan": 0.001,
            "feb": 0.001,
            "mar": 0.001,
            "apr": 0.001,
            "may": 0.001,
            "june": 0.001,
            "july": 0.001,
            "aug": 0.001,
            "sept": 0.001,
            "oct": 0.001,
            "nov": 0.001,
            "dec": 0.001
        }
    ]
    const columnDef = [
        {headerName: "ET&T Org", field: 'etNtOrg'},
        {headerName: "Manager", field: 'manager'},
        {headerName: "US Focal", field: 'usFocal'},
        {headerName: "Project", field: 'project'},
        {headerName: "Skill Group", field: 'skillGroup'},
        {headerName: "Business Unit", field: 'businessUnit'},
        {headerName: "Capabilities", field: 'capabilities'},
        {headerName: "Chargeline", field: 'chargeLine'},
        {headerName: "Forecast Confidence", field: 'forecastConfidence'},
        {headerName: "Comments", field: 'comments'},
        {headerName: "January", field: 'jan'},
        {headerName: "February", field: 'feb'},
        {headerName: "March", field: 'mar'},
        {headerName: "April", field: 'apr'},
        {headerName: "May", field: 'may'},
        {headerName: "June", field: 'june'},
        {headerName: "July", field: 'july'},
        {headerName: "August", field: 'aug'},
        {headerName: "September", field: 'sept'},
        {headerName: "October", field: 'oct'},
        {headerName: "November", field: 'nov'},
        {headerName: "December", field: 'dec'},
    ]
    return (
        <div style={{height: "90vh", width: "100%"}} className={"ag-theme-material"}>
            <AgGridReact columnDefs={columnDef} rowData={data}/>
        </div>
    );
}

export default Worksheet;