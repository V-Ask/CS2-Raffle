import type {WorkshopMap} from "@/models/workshop-map.ts";
import type {GameHosts} from "@/models/game-hosts.ts";

export interface GameHostService {
  startServer(password: string): Promise<boolean>
  stopServer(password: string): Promise<boolean>
  setWorkshopMap(workshopMap: WorkshopMap, password: string): Promise<boolean>
  testConnection(password: string): Promise<boolean>
  getType(): GameHosts
  saveConfig(): void
  isGameHostSetup(): boolean
}
