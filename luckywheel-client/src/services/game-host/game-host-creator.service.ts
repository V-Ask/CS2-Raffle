import {DathostGameHostService} from "@/services/game-host/dathost-game-host.service.ts";
import {GameHosts} from "@/models/game-hosts.ts";
import StorageService from "@/services/storage.service.ts";
import StorageKeys from "@/helpers/constants/storage-keys.ts";
import type {GameHostService} from "@/services/game-host/game-host.service.ts";

export default {
  loadGameHostFromType(type: GameHosts): GameHostService {
    let value = StorageService.getObjectFromStorage(StorageKeys.GAME_HOST_DATA);
    switch (type) {
      case GameHosts.DATHOST:
        return new DathostGameHostService(value.serverId, value.username);
    }
  },

  loadDefaultGameHost() {
    return new DathostGameHostService();
  }
}
