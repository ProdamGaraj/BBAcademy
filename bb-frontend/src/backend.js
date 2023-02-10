import baseurl from "./base-url";
import axios from "axios";

// Promise не будет выбивать в catch, если статусы ответа 200-300, 400 или 500.
const validateStatus = status => (status >= 200 && status <= 300) || [400, 500].includes(status);

export default {
    Course: {
        GetForDashboard: async () => {
            let response = await axios.get(baseurl + '/Course/GetForDashboard', {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            })

            return response.data;
        },
        GetFullInfoForView: async (courseId) => {
            let response = await axios.get(baseurl + '/Course/GetFullInfoForView', {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            })

            return response.data;
        }
    },
    Account: {
        Register: async (data) => {
            let response = await axios.post(baseurl + '/Account/Register', data, {
                withCredentials: true,
                validateStatus
            });

            return response.data;
        },
        Login: async (data) => {
            let response = await axios.post(baseurl + '/Account/Login', data, {
                withCredentials: true,
                validateStatus
            })

            return response.data;
        }
    },
    Cart: {
        GetAll: async () => {
            let response = await axios.post(baseurl + "/Cart/GetAll", {}, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            });

            return response.data;
        },
        RemoveCourse: async (courseId) => {
            let response = await axios.get(baseurl + "/Cart/RemoveCourse", {
                params: {
                    id: courseId
                },
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            });

            return response.data;
        },
        Checkout: async () => {
            let response = await axios.post(baseurl + "/Cart/Checkout", {}, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            });

            return response.data;
        }
    }
}