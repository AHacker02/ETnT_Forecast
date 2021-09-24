import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";
import _ from "lodash";
import api from "../../utils/api";
import {FORECAST} from "../../utils/endpoints";

let cancelToken;

export const getForecast = createAsyncThunk(
    "forecast/get",
    async () => {
        const response = await api.get(FORECAST);
        return response.data.data;
    }
)

export const worksheetSlice = createSlice({
    name: "forecast",
    initialState: {forecast: null},
    extraReducers: {
        [getForecast.fulfilled]: (state, action) => {
            state.forecast = action.payload;
        }
    }
});

export const selectForecast = (state) => state.worksheet.forecast;
export default worksheetSlice.reducer;