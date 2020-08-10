import React from "react";
import { PropTypes } from 'prop-types';

const Footer = ({
    isShown
}) => {
    return (
        <div>
            {isShown ?
                <div className="Footer">
                    <h5 className="title">Footer Content</h5>
                </div> : ''
            }
        </div>
    );
}

Footer.propTypes = {
    isShown: PropTypes.bool,
};

Footer.defaultProps = {
    isShown: false,
};
export default Footer;
