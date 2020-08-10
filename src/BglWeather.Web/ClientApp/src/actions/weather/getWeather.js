import { handleException } from '../index';
import { ApiService } from '../../services';
import { toastr } from 'react-redux-toastr';

export const GET_WEATHER_REQUEST = 'GET_WEATHER_REQUEST';
export const GET_WEATHER_SUCCESS = 'GET_WEATHER_SUCCESS';
export const GET_WEATHER_FAILURE = 'GET_WEATHER_FAILURE';

function requestGetWeather() {
    return {
        type: GET_WEATHER_REQUEST,
        isGettingWearther:true
    };
}

function receiveGetWeather(inputData) {
    return {
        type: GET_WEATHER_SUCCESS,
        isGettingWearther: false,
        weatherData: inputData.result,
    };
}

function rejectGetWeather() {
    return {
        type: GET_WEATHER_FAILURE,
        isGettingWearther: false
    };
}

export function getWeather(location) {
    return (dispatch) => {
        dispatch(requestGetWeather());
        const promise = () => ApiService
            .getWeatherByLocation(location)
            .then((response) => {
                dispatch(receiveGetWeather(response.data));
            })
            .catch((error) => {
                if (!error.response && error.retries && error.retries > 1) {
                    ApiService.addQueuedPromise(promise, error);
                }
                if (error.response || (error.retries && error.retries > 1)) {
                    dispatch(handleException(error, rejectGetWeather, true));
                }
                else {
                    dispatch(handleException(error, rejectGetWeather, true))
                }
            });
        return promise();
    };
}

