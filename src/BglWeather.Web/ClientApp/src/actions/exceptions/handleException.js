import {
  handleConnectionException,
  handleServerException,
  handleUncaughtException
} from '../index';

const EXCEPTION_TYPE = {
  UNCAUGHT: 'UNCAUGHT',
  API_REQUEST: 'API_REQUEST',
  NETWORK: 'NETWORK'
};

// eslint-disable-next-line import/prefer-default-export
export function handleException(
  exception,
  callbackActionCreator,
  showAlert = false,
  showNetworkErrorAlert = true
) {
  if (exception instanceof ErrorEvent) {
    // Uncaught error has happened
    console.error('Uncaught error has happened', exception);

    return handleUncaughtException(exception, callbackActionCreator, showAlert);
  }
  if (exception.response) {
    // The response has been received from the server
    if (exception.response.data && exception.response.data.code === 401) {
      console.warn('API request: Unauthorized');
    } else {
      console.error('Exception from server', exception.response.data);
     
    }
    return handleServerException(exception.response, callbackActionCreator, showAlert);
  }
  // Network exception
  console.error('Network exception', exception);
 
  return handleConnectionException(callbackActionCreator, showNetworkErrorAlert);
}
