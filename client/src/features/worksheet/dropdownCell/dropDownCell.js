import React, {useState} from 'react';
import {Autocomplete, TextField} from "@mui/material";
import {setColValue} from "../worksheetSlice";
import {useDispatch, useSelector} from "react-redux";
import _ from "lodash"

const DropDownCell = (props) => {
    const [inputValue, setInputValue] = useState('');
    const dispatch = useDispatch();
    console.log(props)

    function onChangeHandler(e, value) {
        dispatch(setColValue({id: props.data.id, key: props.colDef.field, value: value}))
    }

    function onInputChangeHandler(e, inputValue) {
        setInputValue(inputValue);
    }

    return (
        <Autocomplete
            options={props.options.map(x=>_.defaultTo(x.value,x.fullName))}
            value={props.value}
            onChange={onChangeHandler}
            inputValue={inputValue}
            onInputChange={onInputChangeHandler}
            disableClearable
            renderInput={(params) => (
                <TextField
                    {...params}
                    style={{padding: '5px 0'}}
                    placeholder={'Select ' + props.column.colId}
                />
            )}
        />
    );
};

export default DropDownCell;