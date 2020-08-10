import { toastr } from 'react-redux-toastr';

export const HANDLE_SERVER_EXCEPTION = 'HANDLE_SERVER_EXCEPTION';

function requestHandleServerException(exception) {
  return {
    type: HANDLE_SERVER_EXCEPTION,
    ...exception
  };
}

export function handleServerException(exception, callbackActionCreator, showAlert) {
  return (dispatch) => {
    if (showAlert) {
      toastr.error(
        'Error',
          exception.data && exception.data.error
              ? "Something's not quite right. Reason: " + exception.data.error.token + ". Please try again."
          : "Something's not quite right. Please try again or speak to your system administrator."
      );
    }
    if (callbackActionCreator && typeof callbackActionCreator === 'function') {
      return dispatch(callbackActionCreator(exception.data));
    }
    return dispatch(requestHandleServerException(exception));
  };
}
