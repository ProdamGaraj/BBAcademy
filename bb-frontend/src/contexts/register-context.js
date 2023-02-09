import React from 'react'

let context = React.createContext({
    edit: {
        Name: '',
        Surname: '',
        Middlename: '',
        Email: '',
        Login: '',
        Password: '',
        ConfirmPassword: '',
    },


    Register: (user) => {
    },
})