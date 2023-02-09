import React from 'react'

let context = React.createContext({
    user: {
        FirstName: '',
        LastName: '',
        MiddleName: '',
        JobTitle: '',
        Organisation: '',
        Rating: 0,
        RecommendedBy: 0,
    },

    setUser: (l) => {
    }
});
export default context;