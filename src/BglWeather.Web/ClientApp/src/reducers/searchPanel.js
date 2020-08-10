import {
    SAVE_SEARCH_CRITERIA,
} from '../actions';
function searchpanel(state = {
    searchCriteria: "",
}, action) {
    switch (action.type) {
        case SAVE_SEARCH_CRITERIA:
            return {
                ...state,
                searchCriteria: action.searchCriteria
            };
        default:
            return state;
    }
}

export default searchpanel;