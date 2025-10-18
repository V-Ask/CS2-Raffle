export class WeightedList<T> {
  items: WeightedItem<T>[];
  cumulativeWeightedMap: WeightedItem<T>[] = [];
  dirty: boolean = true;

  constructor() {
    this.items = [];
  }

  public push(value: T, weight: number) {
    this.dirty = true;
    const weightedItem: WeightedItem<T> = {
      value,
      weight,
    };
    this.items.push(weightedItem);
  }

  public getRandom(): T {
    if (this.items.length === 0) {
      throw new Error('Trying to get a random value from an empty list');
    }
    if (this.dirty) {
      this.buildCDF();
    }
    let cumulatedWeight = this.cumulativeWeightedMap[this.cumulativeWeightedMap.length - 1].weight;
    let randomWeight = Math.random() * cumulatedWeight;
    let index = 0;
    while (randomWeight > this.cumulativeWeightedMap[index].weight) {
      index++;
    }
    return this.cumulativeWeightedMap[index].value;
  }

  private buildCDF() {
    this.dirty = false;
    this.items = this.items.sort((a, b) => a.weight - b.weight);
    this.cumulativeWeightedMap = [];
    let cumulativeWeight = 0;
    this.items.forEach(item => {
      cumulativeWeight += item.weight;
      const cumulativeWeightedItem: WeightedItem<T> = {
        value: item.value,
        weight: cumulativeWeight,
      };
      this.cumulativeWeightedMap.push(cumulativeWeightedItem);
    });
  }

  public static fromArray<T>(array: T[], weightArray: number[]): WeightedList<T> {
    const result = new WeightedList<T>();
    for (let i = 0; i < Math.min(array.length, weightArray.length); i++) {
      result.push(array[i], weightArray[i]);
    }
    return result;
  }

  public static fromArrayMap<T>(array: T[], mappingFunc: (value: T) => number): WeightedList<T> {
    const weightArray = array.map(mappingFunc);
    return WeightedList.fromArray(array, weightArray);
  }

  public static fromSetMap<T>(set: Set<T>, mappingFunc: (value: T) => number): WeightedList<T> {
    const result = new WeightedList<T>();
    set.forEach(element => {
      result.push(element, mappingFunc(element));
    });
    return result;
  }
}

type WeightedItem<T> = {
  value: T,
  weight: number
}
