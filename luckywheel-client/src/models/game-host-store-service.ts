import type {GameHostService} from "@/services/game-host/game-host.service.ts";

export type GameHostStoreService = {
  loadedFromStorage: boolean,
  service: GameHostService
}
