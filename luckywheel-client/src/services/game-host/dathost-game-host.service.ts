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

  constructor(serverId: string = "", username: string = "") {
    this.serverId = serverId;
    this.username = username;
  }

  setWorkshopMap(workshopMap: WorkshopMap, password: string): Promise<boolean> {
    return DatHostApi.setWorkshopMap(this.serverId, this.username, password, workshopMap.mapId);
  }

  startServer(password: string): Promise<boolean> {
    return DatHostApi.startServer(this.serverId, this.username, password);
  }

  stopServer(password: string): Promise<boolean> {
    return DatHostApi.stopServer(this.serverId, this.username, password);
  }

  testConnection(password: string): Promise<boolean> {
    return DatHostApi.testConnection(this.serverId, this.username, password);
  }

  getType(): GameHosts {
    return GameHosts.DATHOST;
  }

  saveConfig(): void {
    const service = new DathostGameHostService(this.serverId, this.username);
    StorageService.saveToStorage(StorageKeys.GAME_HOST_KEY, GameHosts.DATHOST);
    StorageService.saveToStorage(StorageKeys.GAME_HOST_DATA, service);
  }

  isGameHostSetup(): boolean {
    return !!this.serverId && !!this.username;
  }
}
