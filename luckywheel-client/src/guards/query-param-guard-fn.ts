import type {GuardFn} from "@/guards/guard-fn.ts";

export const queryParamGuardFn: (param: string) => GuardFn = (requiredParam: string) => {
  return (from, to) => {
    return !!to.query[requiredParam];

  }
}
