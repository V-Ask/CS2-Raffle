import type {UserCredentials} from "@/models/user-credentials.ts";
import Password from "@/helpers/constants/password.ts";
import {PasswordValidator} from "@/models/password-validator.ts";
import Auth from "@/api/auth.ts";
import {useAuthStore} from "@/stores/auth.store.ts";
import {useLoginStore} from "@/stores/login.store.ts";
import {useRouter} from "vue-router";
import {PLAYLIST_VIEW} from "@/helpers/constants/routing.ts";

function isPasswordValid(password: string): boolean {
  const minLength = Password.MIN_LENGTH;
  let validator = new PasswordValidator(password).validateLengthy(minLength);
  if (Password.MUST_CONTAIN_UPPERCASE) {
    validator = validator.validateUppercaseChar();
  }
  if (Password.MUST_CONTAIN_LOWERCASE) {
    validator = validator.validateLowercaseChar();
  }
  if (Password.MUST_CONTAIN_DIGIT) {
    validator = validator.validateDigit();
  }
  if (Password.MUST_CONTAIN_NON_ALPHANUMERIC) {
    validator = validator.validateNonAlphanumeric();
  }
  return validator.validate();
}

async function loginUser(credentials: UserCredentials) {
  try {
    let response = await Auth.login(credentials.email, credentials.password);
    const authStore = useAuthStore();
    authStore.user = {
      email: credentials.email,
      isConfirmed: false,
    }
    return response.status === 200;
  } catch (e) {
    console.error(e);
    return false;
  }
}

async function registerUser(credentials: UserCredentials) {
  const loginStore = useLoginStore();
  if (!isPasswordValid(credentials.password)) {
    loginStore.setPasswordValidity(false);
    return false;
  }
  loginStore.setPasswordValidity(true);
  return Auth.register(credentials.email, credentials.password).then(response => {
    if (response.status === 200) {
      return loginUser(credentials);
    }
    return false;
  })
    .catch((e) => {
      console.error(e);
      return false;
    })
}

async function checkAuth() {
  return Auth.auth().then(response => {
    if(response.status === 200) {
      return true;
    }
    const authStore = useAuthStore();
    authStore.user = null
    return false;
  }).catch(e=> {
    const authStore = useAuthStore();
    authStore.user = null
    console.error(e);
    return false;
  })
}

export default {
  loginUser,
  registerUser,
  checkAuth,
}
