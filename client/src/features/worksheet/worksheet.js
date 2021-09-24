import React, {useEffect, useState} from 'react';
import {AgGridReact} from "ag-grid-react";
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import './worksheet.css';
import {useDispatch, useSelector} from "react-redux";
import {getForecast, selectForecast} from "./worksheetSlice";


const Worksheet = () => {
    const forecast = useSelector(selectForecast);
    const dispatch = useDispatch();
    const [gridApi, setGridApi] = useState(null);
    const [gridColumnApi, setGridColumnApi] = useState(null);
    const [rowData, setRowData] = useState(null);

    useEffect(()=>{
        debugger;
        setRowData(forecast);
    },[forecast]);

    const onGridReady = (params) => {
        console.log(params);
        setGridApi(params.api);
        setGridColumnApi(params.columnApi);
        dispatch(getForecast());
    };

    const onBtExport = () => {
        gridApi.exportDataAsExcel();
    };

    const columnDef = [
        {headerName: "ET&T Org", field: 'org'},
        {headerName: "Manager", field: 'manager'},
        {headerName: "US Focal", field: 'usFocal'},
        {headerName: "Project", field: 'project'},
        {headerName: "Skill Group", field: 'skillGroup'},
        {headerName: "Business Unit", field: 'business'},
        {headerName: "Capabilities", field: 'capability'},
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
        {headerName: "September", field: 'sep'},
        {headerName: "October", field: 'oct'},
        {headerName: "November", field: 'nov'},
        {headerName: "December", field: 'dec'},
    ]
    const defaultColumnDef = {sortable: true, filter: true, editable: true}
    return (
        <div className="container">
            <div>
                <button
                    onClick={() => onBtExport()}
                    style={{marginBottom: '5px', fontWeight: 'bold'}}
                >
                    Export to Excel
                </button>
            </div>
            <div className="ag-theme-alpine fullwidth-grid">
                <AgGridReact onGridReady={onGridReady} columnDefs={columnDef} rowData={rowData}
                             defaultColDef={defaultColumnDef}/>
            </div>
        </div>
    );
}

export default Worksheet;