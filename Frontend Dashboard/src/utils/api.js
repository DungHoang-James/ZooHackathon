import axios from 'axios';

const BACKEND_URL = 'https://4b92-2402-800-6388-64d3-60b8-f5ef-49a4-a2a0.ngrok.io';

const api = axios.create({ baseURL: BACKEND_URL });

api.interceptors.request.use((req) => {
	const token = localStorage.getItem('unibean-token');
	if (token) {
		req.headers.Authorization = `Bearer ${token}`;
	}
	return req;
});

export default api;
