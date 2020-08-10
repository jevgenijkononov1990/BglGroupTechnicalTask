import {
    GET_WEATHER_REQUEST,
    GET_WEATHER_SUCCESS,
    GET_WEATHER_FAILURE,
} from '../actions'

function weather(state = {
    isGettingWearther: false,
    weatherData: null
}, action){
    switch (action.type) {
        case GET_WEATHER_REQUEST:
            return {
                ...state,
                isGettingWearther: action.isGettingWearther,
                weatherData: null
            };
        case GET_WEATHER_SUCCESS:
            return {
                ...state,
                isGettingWearther: action.isGettingWearther,
                weatherData: action.weatherData,
            };
        case GET_WEATHER_FAILURE:
            return {
                ...state,
                isGettingWearther: action.isGettingWearther,
                weatherData: null
            };
        default:
            return state;
    }
}

export default weather;