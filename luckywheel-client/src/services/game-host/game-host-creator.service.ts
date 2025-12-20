import {DathostGameHostService} from "@/services/game-host/dathost-game-host.service.ts";

export default {
  createDathostService(username: string, password: string) {
    return new DathostGameHostService(username, password);
  },
  createDathostServiceIfValid(username: string, password: string) {
    const service = this.createDathostService(username, password);
    return service.testConnection().then(valid => {
      if (valid) {
        return service;
      }
      return Promise.reject(new Error('Failed to create DathostService'));
    })
  }
}
