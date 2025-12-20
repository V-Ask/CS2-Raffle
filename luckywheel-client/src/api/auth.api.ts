import { API } from './api'
import type {AuthUserDto} from "@/api/dto/auth-user-dto.ts";

async function login(username: string, password: string) {
  return API.post('/api/login?useCookies=true', {
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

async function auth(): Promise<AuthUserDto> {
  return API.get('/api/User/auth').then(response => response.data);
}

async function getInfo() {
  return API.get('/manage/info');
}

export default {
  login,
  register,
  auth
}
