import axios from 'axios'
import router from "@/router";

export const API = axios.create({
  timeout: 1000,
  withCredentials: true,
  headers: {
    "Content-Type": "application/json;charset=UTF-8",
    "Accept": "application/json"
  }
});

API.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response?.status === 401) {
      router.router.push({
        name: "Login"
      }).then();
    }
    return Promise.reject(error);
  }
)
