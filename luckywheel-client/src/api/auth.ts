import { API } from './api'

async function login(username: string, password: string) {
  return API.post('/api/login', {
    email: username,
    password,
  })
}

async function register(username: string, password: string) {
  return API.post('/api/register', {
    email: username,
    password,
  })
}

async function auth() {
  return API.get('/api/User/auth');
}

async function getInfo() {
  return API.get('/manage/info');
}

export default {
  login,
  register,
  auth
}
