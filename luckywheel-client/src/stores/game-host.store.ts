import {defineStore} from "pinia";
import {computed, ref} from "vue"
import {GameHosts} from "@/models/game-hosts.ts";
import type {GameHostService} from "@/services/game-host/game-host.service.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";
import StorageService from "@/services/storage.service.ts";
import StorageKeys from "@/helpers/constants/storage-keys.ts";
import GameHostCreatorService from "@/services/game-host/game-host-creator.service.ts";
import type {GameHostStoreService} from "@/models/game-host-store-service.ts";

export const useGameHostStore = defineStore('game-host', () => {
  const gameHostStoreService = ref<GameHostStoreService>();

  const ensureGameHostService = computed(() => {
    if (!gameHostStoreService.value) {
      loadGameHostIntoStore();
    }
    return gameHostStoreService.value!;
  });

  let dirty = true;
  let successful = false;

  async function startServer() {
    if (hasFailedAndChecked()) return;
    const gameHostService = ensureGameHostService.value.service;
    dirty = false;
    return gameHostService.startServer().then(success => {
      successful = success;
      return success;
    }).catch(_ => {
      successful = false;
    });
  }


  async function stopServer() {
    if (hasFailedAndChecked()) return;
    const gameHostService = ensureGameHostService.value.service;
    dirty = false;
    return gameHostService.stopServer().then(success => {
      successful = success;
      return success;
    }).catch(_ => {
      successful = false;
    });
  }


  async function setWorkshopMap(workshopMap: WorkshopMap) {
    if (hasFailedAndChecked()) return;
    const gameHostService = ensureGameHostService.value.service;
    dirty = false;
    return gameHostService.setWorkshopMap(workshopMap).then(success => {
      successful = success;
      return success;
    }).catch(_ => {
      successful = false;
    });
  }

  function hasPreviouslySucceededOrNotYetChecked() {
    return dirty || successful;
  }

  function hasFailedAndChecked() {
    return !hasPreviouslySucceededOrNotYetChecked();
  }

  function setNewGameHost(service: GameHostService) {
    gameHostStoreService.value = {
      service,
      loadedFromStorage: false,
    };
    dirty = true;
    successful = false;
  }

  /*
  (Re)loads a possibly saved Game Host service into active store, not to
  be confused with Local Storage
   */
  function loadGameHostIntoStore() {
    const hostType = StorageService.getTypeFromStorage<GameHosts>(StorageKeys.GAME_HOST_KEY);
    let storeService:  GameHostStoreService;
    if (hostType) {
      storeService = {
        service: GameHostCreatorService.loadGameHostFromType(hostType as GameHosts),
        loadedFromStorage: true,
      }
    } else {
      storeService = {
        service: GameHostCreatorService.loadDefaultGameHost(),
        loadedFromStorage: false,
      }
    }
    gameHostStoreService.value = storeService;
  }

  return {
    ensureGameHostService,
    startServer,
    stopServer,
    setWorkshopMap,
    setNewGameHost,
    hasFailedAndChecked
  };
})

function gameHostDoesNotExistReject() {
  return Promise.reject(new Error("Game host not found"));
}
