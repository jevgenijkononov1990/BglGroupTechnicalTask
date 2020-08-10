
const baseUrl = document.getElementsByTagName('base')[0].href;

const EnvConfig = {
    baseApiUrl: baseUrl, //  to modify for future
    apiEndpoints: {
        apiPrefix: 'api',
        weather: '/weather'
    },
};
export default EnvConfig;