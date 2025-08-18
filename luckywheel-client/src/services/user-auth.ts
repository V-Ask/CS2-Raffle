import type {UserCredentials} from "@/models/user-credentials.ts";
import {useAuthStore} from "@/stores/auth.ts";
import Password from "@/helpers/constants/password.ts";
import {PasswordValidator} from "@/models/password-validator.ts";
import {useLoginStore} from "@/stores/login.ts";
import Auth from "@/api/auth.ts";

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

function loginUser(credentials: UserCredentials) {
  Auth.login(credentials.email, credentials.password).then(response => {
    const authStore = useAuthStore();
    authStore.user = {
      email: credentials.email,
      isConfirmed: false,
    }
    return response.status === 200;
  })
    .catch((e) => {
      console.error(e);
      return false;
    })
}

async function registerUser(credentials: UserCredentials) {
  const loginStore = useLoginStore();
  if (!isPasswordValid(credentials.password)) {
    loginStore.setPasswordValidity(false);
    return false;
  }
  loginStore.setPasswordValidity(true);
  Auth.register(credentials.email, credentials.password).then(response => {
    if (response.status === 200) {
      return loginUser(credentials);
    }
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
  }).catch(e => {
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
