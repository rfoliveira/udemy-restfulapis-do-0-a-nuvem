import Axios from 'axios';

const api = Axios.create({
    baseURL: 'https://localhost:5001/'
});

export default api;