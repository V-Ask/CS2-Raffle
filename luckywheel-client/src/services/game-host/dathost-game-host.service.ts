import type {GameHostService} from "@/services/game-host/game-host.service.ts";
import {type WorkshopMap} from "@/models/workshop-map.ts";
import DatHostApi from "@/api/game-hosts/dat-host.api.ts";
import {GameHosts} from "@/models/game-hosts.ts";
import password from "@/helpers/constants/password.ts";
import StorageService from "@/services/storage.service.ts";
import StorageKeys from "@/helpers/constants/storage-keys.ts";

export class DathostGameHostService implements GameHostService {
  serverId: string;
  username: string;
  password: string;

  constructor(serverId: string = "", username: string = "", password: string = "") {
    this.serverId = serverId;
    this.username = username;
    this.password = password;
  }

  setWorkshopMap(workshopMap: WorkshopMap): Promise<boolean> {
    return DatHostApi.setWorkshopMap(this.serverId, this.username, this.password, workshopMap.mapId);
  }

  startServer(): Promise<boolean> {
    return DatHostApi.startServer(this.serverId, this.username, this.password);
  }

  stopServer(): Promise<boolean> {
    return DatHostApi.stopServer(this.serverId, this.username, this.password);
  }

  testConnection(): Promise<boolean> {
    return DatHostApi.testConnection(this.serverId, this.username, this.password);
  }

  getType(): GameHosts {
    return GameHosts.DATHOST;
  }

  saveConfig(): void {
    StorageService.saveToStorage(StorageKeys.GAME_HOST_KEY, GameHosts.DATHOST);
    StorageService.saveToStorage(StorageKeys.GAME_HOST_DATA, this);
  }
}
