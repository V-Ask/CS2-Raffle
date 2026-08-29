import {DathostGameHostService} from "@/services/game-host/dathost-game-host.service.ts";
import {GameHosts} from "@/models/game-hosts.ts";
import StorageService from "@/services/storage.service.ts";
import StorageKeys from "@/helpers/constants/storage-keys.ts";

export default {
  loadGameHostFromType(type: GameHosts) {
    let value = StorageService.getObjectFromStorage(StorageKeys.GAME_HOST_DATA);
    switch (type) {
      case GameHosts.DATHOST:
        return new DathostGameHostService(value.serverId, value.username, value.password);
    }
  },

  loadDefaultGameHost() {
    return new DathostGameHostService();
  }
}
