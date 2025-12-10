import type {Color} from "@/types/color.ts";
import {ReelMap} from "@/models/reel-map.ts";
import type {WorkshopPlaylistMapViewDto} from "@/api/dto/views/workshop-playlist-map-view-dto.ts";

export class WorkshopPlaylistMapView {
  public constructor(
    public name: string,
    public imageUrl: string,
    public description: string,
    public mapId: string,
    public playlistId: string,
    public weight: number,
    public hasPlayed: boolean,
  ) {
  }

  public static fromDto(dto: WorkshopPlaylistMapViewDto): WorkshopPlaylistMapView {
    return new WorkshopPlaylistMapView(
      dto.name,
      dto.imageUrl,
      dto.description,
      dto.mapId,
      dto.playlistId,
      dto.weight,
      dto.hasPlayed,
    )
  }

  public toReelMap(color: Color): ReelMap {
    return new ReelMap(
      this.name,
      this.imageUrl,
      this.mapId,
      this.weight,
      color
    );
  }
}
