import type {RouteLocationNormalizedLoadedGeneric} from "vue-router";

export type GuardFn = (from: RouteLocationNormalizedLoadedGeneric, to: RouteLocationNormalizedLoadedGeneric) => boolean | Promise<boolean>;

export function inverseGuardFn(guard: GuardFn): GuardFn {
  return (from, to) => !guard(from, to);
}
