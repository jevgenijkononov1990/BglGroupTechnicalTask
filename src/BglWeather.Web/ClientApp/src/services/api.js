import axios from 'axios';
import EnvConfig from '../env.config';

const {
    baseApiUrl,
    apiEndpoints: {
        apiPrefix: apiPrefixName,
        weather:weatherUri,
    }
} = EnvConfig;

export class ApiService {
    static axios = axios.create({
        baseUrl: baseApiUrl
    });

    static getWeatherByLocation(location) {
        return ApiService.axios
            .get(
                `${baseApiUrl}${apiPrefixName}${weatherUri}?location=${location}`
            );
    }
    /* Retry Code */
    static queuedPromises = [];

    static isProcessingQueue = false;

    static startQueuedRequestRetryTimeout = 0;

    static globalRetryCount = 0;

    static insertQueuedPromise(promise) {
        if (promise != null) {
            if (typeof promise.priority === 'undefined') {
                promise.priority = 99;
            }

            const idx = ApiService.queuedPromises.findIndex(x => !x.priority || x.priority < promise.priority);
            ApiService.queuedPromises.splice(idx, 0, promise);
        }
    }

    static addQueuedPromise(promise, error) {
        if (promise != null) {
            ApiService.queuedPromises.push(promise);
        }
    }

    static retryPromise(promise) {
        if (!promise) {
            return Promise.reject(new Error('Empty promise'));
        }

        ApiService.globalRetryCount += 1;
        return Promise.resolve(promise());
    }

    static retryRequest(request) {
        if (!request) {
            return Promise.reject(new Error('Empty promise'));
        }

        const requestConfig = { ...request, retries: (request.retries || 0) + 1 };
        ApiService.globalRetryCount += 1;
        return Promise.resolve(ApiService.axios.request(requestConfig));
    }

    static delayRetryRequest(ms, requestConfig) {
        return new Promise(resolve => setTimeout(resolve, ms)).then(() => ApiService.retryRequest(requestConfig));
    }

    static processQueuedPromises(bypassCheck) {
        if (!bypassCheck && ApiService.isProcessingQueue) {
            return;
        }

        ApiService.isProcessingQueue = true;
        ApiService.startQueuedRequestRetryTimeout = 0;
        clearTimeout(ApiService.startQueuedRequestRetryTimeout);

        if (ApiService.queuedPromises && ApiService.queuedPromises.length > 0) {
            const request = ApiService.queuedPromises.shift();
            ApiService.retryPromise(
                request
            ).then(() => {
                // Should only hit here if successful?
                ApiService.startQueuedRequestRetryTimeout = 0;
                clearTimeout(ApiService.startQueuedRequestRetryTimeout);
                ApiService.isProcessingQueue = false;
                if (ApiService.globalRetryCount < 2) {
                    ApiService.processQueuedPromises(false);
                }
            }).catch((error) => {
                ApiService.isProcessingQueue = false;
                Promise.reject(error);
            });
        } else {
            ApiService.isProcessingQueue = false;
        }
    }
}