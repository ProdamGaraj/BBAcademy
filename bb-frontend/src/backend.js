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
        }
    },
    Account: {
        Register: async (data) => {
            let response = await axios.post(baseurl + '/Account/Register', {
                body: JSON.stringify(data),
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            });

            return response.data;
        },
        Login: async (data) => {
            let response = await axios.post(baseurl + '/Account/Login', {
                body: JSON.stringify(data),
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            })

            return response.data;
        }
    }
}