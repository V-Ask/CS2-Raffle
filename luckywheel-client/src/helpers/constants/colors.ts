import type {Color} from "@/types/color.ts";

export const COLOR_RARITY_PAIRS: { chance: number, color: Color }[] = [
  {chance: 45, color: 'rgb(128, 128, 128)'},
  {chance: 32.5, color: 'rgb(0, 100, 255)'},
  {chance: 15, color: 'rgb(128, 0, 128)'},
  {chance: 10, color: 'rgb(255, 20, 147)'},
  {chance: 2, color: 'rgb(255, 0, 0)'},
  {chance: 0.5, color: 'rgb(255, 215, 0)'},
]

export const getColorFromRarity: (rarity: number) => Color = rarity => {
  for (const colorRarityPair of COLOR_RARITY_PAIRS) {
    if (rarity >= 100 - colorRarityPair.chance) {
      return colorRarityPair.color;
    }
  }
  return COLOR_RARITY_PAIRS[COLOR_RARITY_PAIRS.length - 1].color;
}
