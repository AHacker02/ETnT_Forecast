import React, {forwardRef, useEffect, useImperativeHandle, useState} from 'react';
import {AgGridReact} from "ag-grid-react";
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import './worksheet.css';
import {useDispatch, useSelector} from "react-redux";
import {
    getForecast, selectBusiness,
    selectCapability, selectCategory,
    selectForecast,
    selectOrgs,
    selectProjects, selectSkills,
    selectUsers,
    setColValue
} from "./worksheetSlice";
import {Dropdown} from "semantic-ui-react";
import DropDownCell from "./dropdownCell/dropDownCell";

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
    useEffect(() => {
        setRowData(forecast);
        autoSizeAll();
    }, [forecast]);

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
    };

    const onBtExport = () => {
        gridApi.exportDataAsExcel();
    };
    const valueSetters = params => {
        dispatch(setColValue({id: params.data.id, key: params.colDef.field, value: params.newValue}))
    };

    const columnDef = [
        {headerName: "ET&T Org", field: 'org', cellRenderer: 'dropDownRenderer', cellRendererParams: {options: useSelector(selectOrgs)},editable:false},
        {headerName: "Manager", field: 'manager', cellRenderer: 'dropDownRenderer', cellRendererParams: {options: useSelector(selectUsers)},editable:false},
        {headerName: "US Focal", field: 'usFocal', cellRenderer: 'dropDownRenderer', cellRendererParams: {options: useSelector(selectUsers)},editable:false},
        {headerName: "Project", field: 'project', cellRenderer: 'dropDownRenderer', cellRendererParams: {options: useSelector(selectProjects)},editable:false},
        {headerName: "Skill Group", field: 'skillGroup', cellRenderer: 'dropDownRenderer', cellRendererParams: {options: useSelector(selectSkills)},editable:false},
        {headerName: "Business Unit", field: 'business', cellRenderer: 'dropDownRenderer', cellRendererParams: {options: useSelector(selectBusiness)},editable:false},
        {headerName: "Capabilities", field: 'capability', cellRenderer: 'dropDownRenderer', cellRendererParams: {options: useSelector(selectCapability)},editable:false},
        {headerName: "Chargeline", field: 'chargeLine'},
        {headerName: "Forecast Confidence", field: 'forecastConfidence', cellRenderer: 'dropDownRenderer', cellRendererParams: {options: useSelector(selectCategory)},editable:false},
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

    const onCellEditingStopped = (e) => {
        props.setIsDirty(true);
    }
    const defaultColumnDef = {sortable: true, filter: true, editable: true, valueSetter: valueSetters}
    return (
        <div className="container">
            <div className="ag-theme-alpine fullwidth-grid">
                <AgGridReact
                    onGridReady={onGridReady}
                    columnDefs={columnDef}
                    rowData={rowData}
                    defaultColDef={defaultColumnDef}
                    onCellEditingStopped={onCellEditingStopped}
                    frameworkComponents={{dropDownRenderer: DropDownCell}}
                />
            </div>
        </div>
    );
});

export default Worksheet;