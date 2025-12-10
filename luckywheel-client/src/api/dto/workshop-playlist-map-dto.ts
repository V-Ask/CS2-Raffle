export class WorkshopPlaylistMapDto {
  public constructor(
    public mapId: string,
    public playlistId: string,
    public weight: number,
    public hasPlayed: boolean,
  ) { }
  }
