import { toastr } from 'react-redux-toastr';

export const HANDLE_CONNECTION_EXCEPTION = 'HANDLE_CONNECTION_EXCEPTION';

const connectionException = {
  code: 0,
  title: 'Network error',
  message: 'Something went wrong during sending request to the server. Please check your internet connection or try again later.'
};

function requestHandleConnectionException() {
  return {
    type: HANDLE_CONNECTION_EXCEPTION,
    code: 0,
    message: connectionException.message
  };
}

export function handleConnectionException(callbackActionCreator, showNetworkErrorAlert) {
    return (dispatch) => {
    if (showNetworkErrorAlert) {
        toastr.error(connectionException.title, connectionException.message);
        console.log("toast");
    }
    if (callbackActionCreator && typeof callbackActionCreator === 'function') {
      return dispatch(callbackActionCreator(connectionException));
    }
    return dispatch(requestHandleConnectionException());
  };
}
