import React, { Component } from 'react';
import { PropTypes } from 'prop-types';

class Weather extends Component {
    static propTypes = {
        weatherDataToDisplay: PropTypes.shape(),
    };

    static defaultProps = {
        weatherDataToDisplay: null
    }

    render() {
        const {
            weatherDataToDisplay,
        } = this.props;
        return (
            <div>
                {!weatherDataToDisplay !== null ?
                    ( <div>
                        <div className="location-box">
                            <div className="location"> {weatherDataToDisplay.location} </div>
                            <div className="date">Sunrise {weatherDataToDisplay.sunrise}</div>
                            <div className="date">Sunset  {weatherDataToDisplay.sunset}</div>
                            <div className="date">Humidity {weatherDataToDisplay.humidity}</div>
                            <div className="date">Pressure  {weatherDataToDisplay.pressure}</div>
                    </div>
                    <div className="weather-box">
                        <div className="temp">
                                {weatherDataToDisplay.temperature.current}°c
                        </div>
                            <div className="weather">Min {weatherDataToDisplay.temperature.minimum}°c </div>
                            <div className="weather">Max {weatherDataToDisplay.temperature.maximum}°c</div>
                        </div>
                    </div>) : ("SORRY NO DATA TO DISPLAY")
                }
            </div>
        );
    }
}
export default Weather;