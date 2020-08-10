import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';
import { reducer as toastrReducer } from 'react-redux-toastr';

import weather from './weather';
import exceptions from './exceptions';
import searchpanel from './searchPanel';



function reduceReducers(...reducers) {
    return (previous, current) => reducers.reduce((p, r) => r(p, current), previous);
}

export default combineReducers({
    exceptions,
    weather,
    searchpanel,
    routing: routerReducer,
    toastr: toastrReducer
});



