import React from "react";
import { PropTypes } from 'prop-types';

const Header = ({
    isShown
}) => {
    return (
        <div>
            {isShown ?
                <div className="Header">
                    <h5 className="title">Header Content</h5>
                </div> : ''
            }
        </div>
    );
}

Header.propTypes = {
    isShown: PropTypes.bool,
};

Header.defaultProps = {
    isShown: false,
};
export default Header;