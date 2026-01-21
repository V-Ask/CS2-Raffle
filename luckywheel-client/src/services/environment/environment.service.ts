
const environment = import.meta.env;

function isAspireDevelopment(): boolean {
  console.log(environment);
  return environment.VITE_IS_ASPIRE_HOST === "true" && isDevelopment();
}

function isDevelopment(): boolean {
  return environment.DEV;
}

export default {
  isAspireDevelopment,
  isDevelopment
}
