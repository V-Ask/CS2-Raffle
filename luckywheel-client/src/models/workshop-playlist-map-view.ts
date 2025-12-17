import type {Color} from "@/types/color.ts";
import {ReelMap} from "@/models/reel-map.ts";
import type {WorkshopPlaylistMapViewDto} from "@/api/dto/views/workshop-playlist-map-view-dto.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";

export class WorkshopPlaylistMapView extends WorkshopMap {
  public constructor(
    public name: string,
    public imageUrl: string,
    public description: string,
    public mapId: string,
    public playlistId: string,
    public weight: number,
    public hasPlayed: boolean,
  ) {
    super(name, imageUrl, description, mapId);
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
      this.description,
      this.mapId,
      this.weight,
      color
    );
  }
}
