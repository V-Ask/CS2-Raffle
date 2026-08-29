import UserAuthService from "@/services/user-auth.service.ts";
import type {GuardFn} from "@/guards/guard-fn.ts";

export const authGuardFn: GuardFn = async (from, to) => {
  try {
    return await UserAuthService.checkAuth();
  } catch (e) {
    console.error(e);
    return false;
  }
}
