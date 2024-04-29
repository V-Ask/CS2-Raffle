<script>
import axios from 'axios'
import { ref } from 'vue'

export default {
  data() {
    return {
      inputText: ref(""),
      nonplayed: [],
      played: [],
      reel: [],
      COPYRIGHT: "© All images from Counter-Strike 2 and the Steam Workshop are property of Valve Corporation. This website is not affiliated with Valve Corporation.",
      ITEMS_PER_PAGE: 5
    }
  },
  methods: {
    async getTest() {
      const path = 'http://localhost:5000/test';
      axios.get(path, 
        {
          params: {
            world: 'hello'
          }
        })
        .then((res) => {
          console.log(res.data);
        })
        .catch((error) => {
          console.error(error);
        });
    },

    async getNonplayed() {
      const path = 'http://localhost:5000/nonplayed';
      axios.get(path)
        .then((res) => {
          console.log(res.data);
          this.nonplayed = res.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    async getPlayed() {
      const path = 'http://localhost:5000/played';
      axios.get(path)
        .then((res) => {
          console.log(res.data);
          this.played = res.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },

    async getReel(reel_length) {
      const path = 'http://localhost:5000/randommaps';
      axios.get(path, {
        reel_length: reel_length
      })
        .then((res) => {
          this.reel = res.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },

    async addMap() {
      const path = 'http://localhost:5000/submitmap';
      axios.post(path, {
        workshop_url: this.inputText
      })
        .then(() => {
          this.getNonplayed();
        })
        .catch((error) => {
          console.error(error);
        });
    },

    async startMap(workshop_id) {
      const path = 'http://localhost:5000/startmap';
      axios.put(path, {
        workshop_id: workshop_id
      })
        .then(() => {
          this.getNonplayed();
        })
        .catch((error) => {
          console.error(error);
        });
    },

    async removeMap(workshop_id) {
      const path = 'http://localhost:5000/submitmap';
      axios.post(path, {
        workshop_id: workshop_id
      })
        .then(() => {
          this.getNonplayed();
          this.getPlayed();
        })
        .catch((error) => {
          console.error(error);
        });
    }
  },
  beforeMount() {
    this.getTest();
    this.getNonplayed();
    this.getPlayed();
  }
}



</script>
<style>
html,
body {
  height: 100vh;
  color: white;
  font: 16px Arial, sans-serif;
  overflow: hidden;
}

body {
  margin: 0;
}

.background {
  height: 100vh;
  background-image: url("https://cdn.cloudflare.steamstatic.com/steam/apps/730/header.jpg?t=1698860631");
  background-attachment: fixed;
  background-size: cover;
  background-position: center top;
}

.blur {
  height: 100%;
  margin: 0;
  backdrop-filter: blur(10px);
  -webkit-backdrop-filter: blur(10px);
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  align-items: center;
}

.input-container {
  width: 60vh;
  display: flex;
  justify-content: center;
  flex-direction: column;
}

.map-input {
  display: flex;
  justify-content: center;
  gap: 10px;
}

.map-input>* {
  width: 100%;
  height: 100%;
}

.reel-container {
  height: 151px;
  position: relative;
  width: calc(v-bind(ITEMS_PER_PAGE) * 268px);
  border: 2px solid white;
  overflow: hidden;
  display: flex;
  flex-direction: column;
}

.reel {
  height: 100%;
  display: flex;
  animation: moveleft 2s linear 1s;
}

.reel>* {
  flex: 0 0 268px;
  background-image: url("https://cdn.cloudflare.steamstatic.com/steam/apps/730/header.jpg?t=1698860631");
  background-size: 100%;
  height: 100%;
  margin: 0;
  color: black;
}

@keyframes moveleft {
  0% {
    transform: translateX(0);
  }

  100% {
    transform: translateX(-2000px);
  }
}

footer {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  text-align: center;
  padding: 10px;
  background-color: rgba(0, 0, 0, 0.5);
  color: white;
}
</style>
<template>
  <body>
    <div class="background">
      <div class="blur">
        <div class="reel-container">
          <div class="reel">
            <p>MAP</p>
            <p>MAP</p>
            <p>MAP</p>
            <p>MAP</p>
            <p>MAP</p>
            <p>MAP</p>
            <p>MAP</p>
            <p>MAP</p>
            <p>MAP</p>
          </div>
        </div>
        <div class="input-container">
          <span>Add Map to Pool:</span>
          <div class="map-input">
            <input v-model="inputText" placeholder="Insert Steam Workshop Link..." />
            <button @click="addMap()">Submit Map</button>
          </div>
        </div>
        <div class="roll-button">
          <input type="button" value="Roll" />
        </div>
      </div>
    </div>
  </body>

  <footer>
    <p>{{ COPYRIGHT }}</p>
  </footer>
</template>