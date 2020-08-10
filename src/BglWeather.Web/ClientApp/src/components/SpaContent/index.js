import React, { Component } from 'react';
import { PropTypes } from 'prop-types';
import { connect } from 'react-redux';
import Weather from '../Weather'
import SearchPanel from '../SearchPanel'
import LoadingWrapper from '../Common/LoadingWrapper';
import { saveSearchCriteria, getWeather } from '../../actions';
import ReduxToastr from 'react-redux-toastr';

class SpaContent extends Component {
    static propTypes = {
        dispatch: PropTypes.func.isRequired,
        isGettingWearther: PropTypes.bool,
        weatherData: PropTypes.shape({}),
        searchCriteria: PropTypes.string
    };

    static defaultProps = {
        isGettingWearther: false,
        weatherData: null,
        searchCriteria: "",
    };

    constructor(props) {
        super(props);
        this.state = { 
            searchCriteria : ""
        };
    }

    onSearchCriteriaChanged = (newSearchCriteria) => {
        const { dispatch } = this.props;
        const { searchCriteria } = this.state;

        this.setState({
            searchCriteria: newSearchCriteria
        });

        dispatch(saveSearchCriteria(newSearchCriteria));
    }

    fetchWeatherDataByLocation = () => {

        console.log("fetchWeatherDataByLoca");
        const { searchCriteria } = this.state;
        const { dispatch } = this.props;
        
        if (searchCriteria.length !== 0) {
            dispatch(getWeather(searchCriteria));
        }
        else {
            console.log("empty search");
        }
    };

    render() {
        const { dispatch, isGettingWearther, weatherData, searchCriteria } = this.props;
        return (
            <div >
                <main>
                    {isGettingWearther ?
                        (<LoadingWrapper loaded={!isGettingWearther} />)
                    :
                        (<SearchPanel
                            searchCriteria={searchCriteria}
                            onSearchCriteriaChanged={this.onSearchCriteriaChanged}
                            onSearchClicked={this.fetchWeatherDataByLocation}
                         /> 
                        )
                    }
                    {!isGettingWearther && weatherData !==null ?
                        (<Weather weatherDataToDisplay={weatherData} />) : ('')
                    }

                </main>
                <ReduxToastr
                    preventDuplicates
                    position="top-right"
                    transitionIn="fadeIn"
                    transitionOut="fadeOut"/>
            </div>
            )
    }
}
const mapDispatchToProps = dispatch => ({ dispatch });

const mapStateToProps = (state) => {
    const { weather, searchpanel } = state;
    const { isGettingWearther, weatherData } = weather;
    const { searchCriteria } = searchpanel;
    return {
        searchCriteria,
        isGettingWearther,
        weatherData
    };
};

const SpaWebContent = connect(
    mapStateToProps,
    mapDispatchToProps,
)(SpaContent);

export default SpaWebContent;