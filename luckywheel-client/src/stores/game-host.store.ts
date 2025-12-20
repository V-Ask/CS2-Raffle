import {defineStore} from "pinia";
import {GameHosts} from "@/models/game-hosts.ts";
import type {GameHostService} from "@/services/game-host/game-host.service.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";
import {DathostGameHostService} from "@/services/game-host/dathost-game-host.service.ts";

export const useGameHostStore = defineStore('game-host', {
  state: () => ({
    selectedGameHostService: new DathostGameHostService("", "") as GameHostService,
    dirty: true,
    successful: false,
  }),
  actions: {
    async startServer() {
      if (this.hasFailedAndChecked()) return;
      if (this.selectedGameHostService) {
        this.dirty = false;
        return this.selectedGameHostService.startServer().then(success => {
          this.successful = success;
          return success;
        }).catch(_ => {
          this.successful = false;
        });
      }
      return Promise.resolve(undefined);
    },

    async stopServer() {
      if (this.hasFailedAndChecked()) return;
      if (this.selectedGameHostService) {
        this.dirty = false;
        return this.selectedGameHostService.stopServer().then(success => {
          this.successful = success;
          return success;
        }).catch(_ => {
          this.successful = false;
        });
      }
      return gameHostDoesNotExistReject();
    },

    async setWorkshopMap(workshopMap: WorkshopMap) {
      if (this.hasFailedAndChecked()) return;
      if (this.selectedGameHostService) {
        this.dirty = false;
        return this.selectedGameHostService.setWorkshopMap(workshopMap).then(success => {
          this.successful = success;
          return success;
        }).catch(_ => {
          this.successful = false;
        });
      }
      return gameHostDoesNotExistReject();
    },

    hasPreviouslySucceededOrNotYetChecked() {
      return this.dirty || this.successful;
    },

    hasFailedAndChecked() {
      return !this.hasPreviouslySucceededOrNotYetChecked();
    },

    setNewGameHost(service: GameHostService) {
      this.selectedGameHostService = service;
      this.dirty = true;
      this.successful = false;
    }
  }
});

function gameHostDoesNotExistReject() {
  return Promise.reject(new Error("Game host not found"));
}
