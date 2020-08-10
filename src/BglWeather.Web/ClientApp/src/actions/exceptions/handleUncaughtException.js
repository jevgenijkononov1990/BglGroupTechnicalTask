import { toastr } from 'react-redux-toastr';

export const HANDLE_UNCAUGHT_EXCEPTION = 'HANDLE_UNCAUGHT_EXCEPTION';

function requestHandleUncaughtException(exception) {
  return {
    type: HANDLE_UNCAUGHT_EXCEPTION,
    code: 0,
    message: exception.message
  };
}

export function handleUncaughtException(exception, callbackActionCreator, showAlert) {
  return (dispatch) => {
    if (showAlert) {
      toastr.error(
        'Error',
        exception.message
          ? exception.message
          : "Something's not quite right. Please try again or speak to your system administrator."
      );
    }
    if (callbackActionCreator && typeof callbackActionCreator === 'function') {
      return dispatch(callbackActionCreator(exception.data));
    }
    return dispatch(requestHandleUncaughtException(exception));
  };
}
