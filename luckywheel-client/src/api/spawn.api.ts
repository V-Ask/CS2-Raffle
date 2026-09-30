import {API} from "@/api/api.ts";
import type {SpawnCountDto} from "@/api/dto/spawn-count-dto.ts";

async function getSpawnCount(mapId: string): Promise<SpawnCountDto | undefined> {
  return API.get('/api/Spawn', {
    params: {mapId},
    timeout: 5000,
  }).then(response => {
    if (response.status === 200) {
      return response.data;
    }
  }).catch(error => {
    console.error(error);
  });
}

export default {
  getSpawnCount
}
