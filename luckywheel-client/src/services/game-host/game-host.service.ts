import type {WorkshopMap} from "@/models/workshop-map.ts";
import type {GameHosts} from "@/models/game-hosts.ts";

export interface GameHostService {
  startServer(): Promise<boolean>
  stopServer(): Promise<boolean>
  setWorkshopMap(workshopMap: WorkshopMap): Promise<boolean>
  testConnection(): Promise<boolean>
  getType(): GameHosts
  saveConfig(): void
}
