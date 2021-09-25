import React, {forwardRef, useEffect, useImperativeHandle, useState} from 'react';
import {AgGridReact} from "ag-grid-react";
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import './worksheet.css';
import {useDispatch, useSelector} from "react-redux";
import {getForecast, selectForecast, setColValue} from "./worksheetSlice";


const Worksheet = forwardRef((props, ref) => {
    const forecast = useSelector(selectForecast);
    const dispatch = useDispatch();
    const [gridApi, setGridApi] = useState(null);
    const [gridColumnApi, setGridColumnApi] = useState(null);
    const [rowData, setRowData] = useState(null);

    useImperativeHandle(
        ref,
        () => ({
            onBtExport() {
                gridApi.exportDataAsCsv();
            }
        }),
    )
    useEffect(()=>{
        setRowData(forecast);
        autoSizeAll();
    },[forecast]);

    const autoSizeAll = (skipHeader) => {
        var allColumnIds = [];
        gridColumnApi?.getAllColumns().forEach(function (column) {
            allColumnIds.push(column.colId);
        });
        gridColumnApi?.autoSizeColumns(allColumnIds, skipHeader);
    };

    const onGridReady = (params) => {
        console.log(params);
        setGridApi(params.api);
        setGridColumnApi(params.columnApi);
        dispatch(getForecast());
    };

    const onBtExport = () => {
        gridApi.exportDataAsExcel();
    };
    const valueSetters = params => {
        dispatch(setColValue({id:params.data.id, key:params.colDef.field, value:params.newValue}))
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
        {headerName: "Jan", field: 'jan'},
        {headerName: "Feb", field: 'feb'},
        {headerName: "Mar", field: 'mar'},
        {headerName: "Apr", field: 'apr'},
        {headerName: "May", field: 'may'},
        {headerName: "June", field: 'june'},
        {headerName: "July", field: 'july'},
        {headerName: "Aug", field: 'aug'},
        {headerName: "Sept", field: 'sep'},
        {headerName: "Oct", field: 'oct'},
        {headerName: "Nov", field: 'nov'},
        {headerName: "Dec", field: 'dec'},
    ]

    const onCellEditingStopped=(e)=> {
        props.setIsDirty(true);
    }
    const defaultColumnDef = {sortable: true, filter: true, editable: true,valueSetter:valueSetters}
    return (
        <div className="container">
            <div className="ag-theme-alpine fullwidth-grid">
                <AgGridReact onGridReady={onGridReady} columnDefs={columnDef} rowData={rowData}
                             defaultColDef={defaultColumnDef} onCellEditingStopped={onCellEditingStopped}/>
            </div>
        </div>
    );
});

export default Worksheet;