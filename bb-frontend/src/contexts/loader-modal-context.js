import React from "react";

let context = React.createContext({
    isOpen: false,
    showModal: () => {
    }, close: () => {
    }
});
export default context;