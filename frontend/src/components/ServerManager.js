import axios from 'axios'

export default class ServerManager {
  constructor() {
    this._nonplayed = [];
    this._played = [];
  }

  async updateNonplayed() {
    const path = 'http://localhost:5000/nonplayed';
    axios.get(path)
      .then((res) => {
        this.nonplayed = res.data;
      })
      .catch((error) => {
        console.error(error);
      });
  }

  async updatePlayed() {
    const path = 'http://localhost:5000/played';
    axios.get(path)
      .then((res) => {
        this.played = res.data;
      })
      .catch((error) => {
        console.error(error);
      });
  }

  async addMap(workshop_url) {
    const path = 'http://localhost:5000/submitmap';
    axios.post(path, {
      workshop_url: workshop_url
    })
      .then(() => {
        this.updateNonplayed();
      })
      .catch((error) => {
        console.error(error);
      });
  }

  async removeMap(workshop_id) {
    const path = 'http://localhost:5000/removemap';
    axios.delete(path, {
      data: {workshop_id: workshop_id}
    })
      .then(() => {
        this.updateNonplayed();
        this.updatePlayed();
      })
      .catch((error) => {
        console.error(error);
      });
  }

  get played() {
    return this._played;
  }

  set played(played) {
    this._nonplayed = played;
  }

  set nonplayed(nonplayed) {
    this._nonplayed = nonplayed;
  }

  get nonplayed() {
    return this._nonplayed;
  }
}