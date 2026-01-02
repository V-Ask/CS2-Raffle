export default {
  saveToStorage(key: string, value: any) {
    localStorage.setItem(key, JSON.stringify(value));
  },

  getFromStorage(key: string): string | null {
    return localStorage.getItem(key);
  },

  getObjectFromStorage(key: string): any | undefined {
    const value = this.getFromStorage(key);
    if (value) {
      return JSON.parse(value);
    }
  },

  getTypeFromStorage<T>(key: string): T | undefined {
    return this.getObjectFromStorage(key) as T;
  },
}
