import {
  createRouter,
  type RouteLocationNormalizedLoadedGeneric,
  type RouteLocationRaw,
  type Router,
  type RouterOptions
} from "vue-router";
import type {GuardFn} from "@/guards/guard-fn.ts";

export class GuardedRouter {

  private readonly _router: Router;

  constructor(options: RouterOptions) {
    this._router = createRouter(options);
  }

  public guardRoute(guardedRouteName: string, predicate: GuardFn, alternative?: RouteLocationRaw) {
    this._router.beforeEach(async (to, from, next) => {
      if (guardedRouteName != to.name?.toString()) {
        next();
        return;
      }
      if (await predicate(from, to)) {
        next();
        return;
      }
      console.debug("Guard hit! Falling back...")
      if (alternative) {
        next(alternative);
      }
    })
  }

  public guardAllRoutes(predicate: GuardFn, excludes?: string[], alternative?: RouteLocationRaw) {
    this._router.beforeEach(async (to, from, next) => {
      if (excludes && to.name && !excludes.includes(to.name.toString())) {
        next();
        return;
      }
      if (await predicate(from, to)) {
        next();
        return;
      }
      console.debug("Guard hit! Falling back...")
      if (alternative) {
        next(alternative);
      }
    })
  }

  get router(): Router {
    return this._router;
  }
}
