import axios from 'axios'

//TODO: Optimizations: We do not need to pair every post/put with a get request,
//if we already know the result
export default class ServerManager {
  constructor() {
    this._nonplayed = [];
    this._played = [];
    this.totalweight = 0;
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
    this.totalweight = 0;
    return axios.get(path)
      .then((res) => {
        this.nonplayed = res.data;
        for(var map in res.data) {
          this.totalweight += res.data[map].weight;
        }
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
    return axios.get(path)
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
    return axios.post(path, {
      data: { workshop_url: workshop_url }
    })
      .then(res => {
        this.nonplayed[res.data['id']] = res.data;
      })
      .catch((error) => {
        if(error.response.status === 401) {
          window.location.href = '/login'
        } else console.error(error);
      });
  }

  async deleteMap(workshop_id) {

  }

  async playMap(workshop_id) {
    let token = localStorage.getItem('access_token');
    if (token === null) {
      window.location.href = '/login';
      return;
    }
    const path = 'http://localhost:5000/playmap';
    return axios.put(path, {
      data: {workshop_id: workshop_id}
    })
      .then(() => {
        this._played[workshop_id] = this._nonplayed[workshop_id];
        this.totalweight -= this._nonplayed[workshop_id].weight;
        delete this._nonplayed[workshop_id];
        for (const map_id in this._nonplayed) {
          this._nonplayed[map_id].weight++;
          this.totalweight++;
        }
      })
      .catch((error) => {
        console.error(error)
        if(error.response.status === 401) {
          window.location.href = '/login';
        } else console.error(error);
      });
  }

  async unplayMap(workshop_id) {
    let token = localStorage.getItem('access_token');
    if (token === null) {
      window.location.href = '/login';
      return;
    }
    const path = 'http://localhost:5000/unplaymap';
    return axios.put(path, {
      data: {workshop_id: workshop_id}
    })
      .then(() => {
        this.updateNonplayed();
        this.updatePlayed();
      })
      .catch((error) => {
        if(error.response.status === 401) {
          window.location.href = '/login';
        } else console.error(error);
      });
  }

  async startMap(workshop_id) {
    let token = localStorage.getItem('access_token');
    if (token === null) {
      window.location.href = '/login';
      return;
    }
    const path = 'http://localhost:5000/startmap';
    return axios.post(path, {
      data: {workshop_id: workshop_id}
    }).catch((error) => {
      if(error.response.status === 401) {
        window.location.href = '/login';
      } else console.error(error);
    });
  }

  get played() {
    return this._played;
  }

  set played(played) {
    this._played = played;
  }

  set nonplayed(nonplayed) {
    this._nonplayed = nonplayed;
  }

  get nonplayed() {
    return this._nonplayed;
  }
}