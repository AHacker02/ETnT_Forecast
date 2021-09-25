import {createAsyncThunk, createSlice,PayloadAction} from "@reduxjs/toolkit";
import _ from "lodash";
import api from "../../utils/api";
import {FORECAST, UPLOAD} from "../../utils/endpoints";
import async from "async";

let cancelToken;

export const getForecast = createAsyncThunk(
    "forecast/get",
    async () => {
        const response = await api.get(FORECAST);
        return response.data.data;
    }
)

export const uploadForecast = createAsyncThunk("forecast/upload",
    async (file) => {
        const formData = new FormData();
        formData.append("file", file);
        const response = await api.post(UPLOAD, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        return response.data.data;
    })

export const saveForecast = createAsyncThunk("forecast/save",
    async (_,thunkAPI)=>{
    const response = await api.post(FORECAST,JSON.stringify(thunkAPI.getState().worksheet.forecast.filter(x=>x.isEdited)));
    return response.data.data;
    })

export const worksheetSlice = createSlice({
    name: "forecast",
    initialState: {forecast: null},
    reducers: {
        setColValue: (state, action) => {
            const rowId = action.payload.id;
            const forecast = state.forecast.filter(x => x.id == rowId)[0];
            forecast[action.payload.key] = action.payload.value;
            forecast["isEdited"] = true;
        },
    },
    extraReducers: {
        [getForecast.fulfilled]: (state, action) => {
            state.forecast = action.payload;
        },
        [saveForecast.fulfilled]:(state,action)=>{
            console.log(state);
        }
    }
});

export const {setColValue} = worksheetSlice.actions;
export const selectForecast = (state) => state.worksheet.forecast;
export default worksheetSlice.reducer;