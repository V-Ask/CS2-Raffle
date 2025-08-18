import { API } from './api'

async function login(username: string, password: string) {
  return API.post('/login', {
    username,
    password,
  })
}

async function register(username: string, password: string) {
  return API.post('/register', {
    username,
    password,
  })
}

async function auth() {
  return API.get('/auth');
}

async function getInfo() {
  return API.get('/manage/info');
}

export default {
  login,
  register,
  auth
}
