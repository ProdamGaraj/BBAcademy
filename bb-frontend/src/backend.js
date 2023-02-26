import baseurl from "./base-url";
import axios from "axios";

// Promise не будет выбивать в catch, если статусы ответа 200-300
const validateStatus = status => (status >= 200 && status <= 300) || [500].includes(status);

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
        GetForLearning: async (courseId) => {
            let response = await axios.post(`${baseurl}/Course/GetForLearning`, {}, {
                params: {
                    id: courseId
                },
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
        },
        GetUser: async () => {
            let response = await axios.post(baseurl + '/Account/GetUser', {}, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
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
            let response = await axios.post(baseurl + "/Cart/RemoveCourse", {}, {
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

        AddCourse: async (courseId) => {
            let response = await axios.post(baseurl + "/Cart/AddCourse", {}, {
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
    },
    
    Payment:{
        GetUrlForPurchase: async () => {
            let response = await axios.post(baseurl + "/Payment/Pay", {}, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            });

            return response.data;
        }
    },
    
    Exam: {
        SaveCourseExamResults: async (data) => {
            let response = await axios.post(baseurl + '/Exam/SaveCourseExamResults', data, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            })

            return response.data;
        }
    },
    Certificate: {
        GetAllForDashboard: async () => {
            let response = await axios.post(baseurl + '/Certificate/GetAllForDashboard', {}, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                withCredentials: true,
                validateStatus
            })

            return response.data;
        },
        GetCertificate: async (name) => {
            let response = await axios.get(baseurl + '/Certificate/GetCertificate', {
                params: {
                    name: name
                },
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                responseType: 'blob',
                withCredentials: true,
                validateStatus
            })

            return response.data;
        }
    }
}
