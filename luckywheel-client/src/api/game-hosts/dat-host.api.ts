import {API} from "@/api/api.ts";

export default {
  async setWorkshopMap(serverId: string, username: string, password: string, mapId: string): Promise<boolean> {
    let result = await API.put(`/api/Dathost/select`, {
      serverId,
      username,
      password,
      mapId,
    });
    return result.status !== 400;
  },

  async startServer(serverId: string, username: string, password: string): Promise<boolean> {
    let result = await API.put(`/api/Dathost/start`, {
      serverId,
      username,
      password,
    });
    return result.status !== 400;
  },

  async stopServer(serverId: string, username: string, password: string): Promise<boolean> {
    let result = await API.put(`/api/Dathost/stop`, {
      serverId,
      username,
      password,
    });
    return result.status !== 400;
  },

  async testConnection(serverId: string, username: string, password: string): Promise<boolean> {
    let result = await API.post(`/api/Dathost/test`, {
      serverId,
      username,
      password,
    });
    return result.status !== 400;
  },
}
