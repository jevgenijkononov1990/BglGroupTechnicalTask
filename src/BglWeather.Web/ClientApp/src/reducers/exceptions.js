import {
    HANDLE_UNCAUGHT_EXCEPTION,
    HANDLE_CONNECTION_EXCEPTION,
    HANDLE_SERVER_EXCEPTION
} from '../actions';

/**
 * The exceptions reducer
 */
function exceptions(state = {}, action) {
    switch (action.type) {
        case HANDLE_UNCAUGHT_EXCEPTION:
            return {
                ...state,
                code: action.code,
                message: action.message
            };
        case HANDLE_CONNECTION_EXCEPTION:
            return {
                ...state,
                code: action.code,
                message: action.message
            };
        case HANDLE_SERVER_EXCEPTION:
            return {
                ...state,
                code: action.code,
                message: action.message,
                id: action.id
            };
        default:
            return state;
    }
}

export default exceptions;
