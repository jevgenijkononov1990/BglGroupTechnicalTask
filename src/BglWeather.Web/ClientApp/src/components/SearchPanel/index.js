import React from 'react';
import { PropTypes } from 'prop-types';

const SearchPanel = ({ searchCriteria, onSearchCriteriaChanged, onSearchClicked }) => {

    const onSearchClick = evt => {
        if (evt.key === "Enter") {
            onSearchClicked(searchCriteria);
        }
    };
    const onSearchBtnClick = evt => {
            onSearchClicked(searchCriteria);
    };

    const onSearchValueChanged = (e) => {
        console.log("SearchPanel onSearchValueChanged")
        //searchCriteria = e.target.value;
        //const newSearchCriteria = { ...searchCriteria };
        onSearchCriteriaChanged(e.target.value);
    };
    return (
        <div>
            
            <div className="search-box">
                <input
                    type="text"
                    className="search-bar"
                    placeholder="Search..."
                    onChange={e => onSearchValueChanged(e)}
                    value={searchCriteria}
                    onKeyPress={onSearchClick}
                />
                <button
                    className="btnSearch"
                    onClick={() => onSearchBtnClick()}
                    type="button"
                
                    title="Search"
                >Search</button>
            </div>
           
        </div>
    );
};

SearchPanel.propTypes = {
    searchCriteria: PropTypes.string.isRequired,
    onSearchCriteriaChanged: PropTypes.func.isRequired,
    onSearchClicked: PropTypes.func.isRequired
};

SearchPanel.defaultProps = {
};


export default SearchPanel;