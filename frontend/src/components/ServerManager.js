import axios from 'axios'

export default class ServerManager {
  constructor() {
    this._nonplayed = [];
    this._played = [];
    axios.interceptors.request.use((config) => {
      const token = localStorage.getItem('access_token');
      if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
      }
      return config;
    },
    (error => {
      window.location.href = '/login';
      return Promise.reject(error);
    }))
  }

  get_auth_header(token) {
    return {
      'Authorization': token
    };
  }

  async updateNonplayed() {
    const path = 'http://localhost:5000/nonplayed';
    axios.get(path)
      .then((res) => {
        this.nonplayed = res.data;
      })
      .catch((error) => {
        if(error.response.status === 401) {
          window.location.href = '/login';
        } else console.error(error);
      });
  }

  async updatePlayed() {
    let token = localStorage.getItem('access_token')
    if (token === null) {
      window.location.href = '/login';
      return;
    }
    const path = 'http://localhost:5000/played';
    axios.get(path)
      .then((res) => {
        this.played = res.data;
      })
      .catch((error) => {
        if(error.response.status === 401) {
          window.location.href = '/login'
        } else console.error(error);
      });
  }

  async addMap(workshop_url) {
    const path = 'http://localhost:5000/submitmap';
    axios.post(path, {
      data: { workshop_url: workshop_url }
    })
      .then(() => {
        this.updateNonplayed();
      })
      .catch((error) => {
        if(error.response.status === 401) {
          window.location.href = '/login'
        } else console.error(error);
      });
  }

  async removeMap(workshop_id) {
    let token = localStorage.getItem('access_token')
    if (token === null) {
      window.location.href = '/login';
      return;
    }
    const path = 'http://localhost:5000/removemap';
    axios.delete(path, {
      data: {workshop_id: workshop_id}
    })
      .then(() => {
        this.updateNonplayed();
        this.updatePlayed();
      })
      .catch((error) => {
        if(error.response.status === 401) {
          window.location.href = '/login'
        } else console.error(error);
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