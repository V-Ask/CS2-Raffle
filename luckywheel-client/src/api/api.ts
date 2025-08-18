import axios from 'axios'
import {useRouter} from "vue-router";

const API_BASE_URL = 'https://localhost:8080'
const router = useRouter();

axios.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if(error.response.status === 401) {
      router.push({
        name: 'Login'
      }).then();
    }
    return error;
  })

export const API = axios.create({
  baseURL: API_BASE_URL,
  timeout: 1000,
});
