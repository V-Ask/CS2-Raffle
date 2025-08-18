import type {RouteLocationNormalizedLoadedGeneric} from "vue-router";
import UserAuth from "@/services/user-auth.ts";

export default async function (from: RouteLocationNormalizedLoadedGeneric, to: RouteLocationNormalizedLoadedGeneric) {
  return UserAuth.checkAuth().then(response => {
    return response;
  }).catch(e => {
    console.error(e);
    return false;
  })
}
