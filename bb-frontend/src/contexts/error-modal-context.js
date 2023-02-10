import React from "react";

let context = React.createContext({
    isOpen: false,
    message: '',
    showModal: (message) => {
    }, close: () => {
    }
});
export default context;