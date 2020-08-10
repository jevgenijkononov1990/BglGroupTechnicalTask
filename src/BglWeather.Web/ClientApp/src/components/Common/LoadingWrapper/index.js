import React from 'react';
import { PropTypes } from 'prop-types';

const LoadingWrapper = ({ children, loaded }) => {
    if (!loaded) {
        return (
            <div className="loader">
                <div>
                    <div>
                        <div className="sk-fading-circle">
                            <div className="sk-circle1 sk-circle" />
                            <div className="sk-circle2 sk-circle" />
                            <div className="sk-circle3 sk-circle" />
                            <div className="sk-circle4 sk-circle" />
                            <div className="sk-circle5 sk-circle" />
                            <div className="sk-circle6 sk-circle" />
                            <div className="sk-circle7 sk-circle" />
                            <div className="sk-circle8 sk-circle" />
                            <div className="sk-circle9 sk-circle" />
                            <div className="sk-circle10 sk-circle" />
                            <div className="sk-circle11 sk-circle" />
                            <div className="sk-circle12 sk-circle" />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
    return (
        <div>
            {children}
        </div>
    );
};

LoadingWrapper.propTypes = {
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node
    ]),
    loaded: PropTypes.bool.isRequired
};

LoadingWrapper.defaultProps = {
    children: null
};

export default LoadingWrapper;
