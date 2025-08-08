export class WorkshopMap {
  workshopName: string;
  workshopId: string;
  rarity: number;
  image: ArrayBuffer;

  constructor(workshopName: string, workshopId: string, rarity: number, image: ArrayBuffer) {
    this.workshopName = workshopName;
    this.workshopId = workshopId;
    this.rarity = rarity;
    this.image = image;
  }
}
