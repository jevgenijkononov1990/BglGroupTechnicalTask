
export const SAVE_SEARCH_CRITERIA = 'SAVE_SEARCH_CRITERIA';

export function saveSearchCriteria(searchCriteria) {
    return {
        type: SAVE_SEARCH_CRITERIA,
        searchCriteria
    };
}

