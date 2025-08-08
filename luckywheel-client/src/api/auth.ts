import { API } from './api'

export async function loginPost(username: string, password: string) {
  return API.post('/login', {
    username,
    password,
  })
}

export async function registerPost(username: string, password: string) {
  return API.post('/register', {
    username,
    password,
  })
}
