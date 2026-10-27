import type {WorkshopPlaylistMapViewDto} from "@/api/dto/views/workshop-playlist-map-view-dto.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";

export class WorkshopPlaylistMapView extends WorkshopMap {
  public constructor(
    public name: string,
    public imageUrl: string,
    public description: string,
    public mapId: string,
    public weight: number,
    public hasPlayed: boolean,
    public rating: number,
    public terroristSpawns?: number,
    public counterTerroristSpawns?: number,
  ) {
    super(name, imageUrl, description, mapId);
  }

  public static fromDto(dto: WorkshopPlaylistMapViewDto): WorkshopPlaylistMapView {
    return new WorkshopPlaylistMapView(
      dto.name,
      dto.imageUrl,
      dto.description,
      dto.mapId,
      dto.weight,
      dto.hasPlayed,
      dto.rating,
      dto.terroristSpawns,
      dto.counterTerroristSpawns,
    );
  }


}
