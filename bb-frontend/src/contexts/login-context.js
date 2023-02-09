import React from 'react'

let context = React.createContext({
    edit:{
        Login:'',
        Password:'',
    },

    Login: (user) => {
    },
});
export default context;