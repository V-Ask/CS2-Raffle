<script>
import axios from 'axios';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css'
import ReelComponent from './ReelComponent.vue';
import ServerManager from './ServerManager'
import MapInputComponent from './MapInputComponent.vue';


//TODO: Add frontend for starting maps and make warning errors stop (parse
//errors as strings)
export default {
  data() {
    return {
      isLoading: false,
      fullPage: true,
      manager: new ServerManager(),
      ITEMS_PER_PAGE: 5,
      COPYRIGHT: "© All images from Counter-Strike 2 and the Steam Workshop are property of Valve Corporation. This website is not affiliated with Valve Corporation.",
    }
  },
  components: {
    Loading,
    ReelComponent,
    MapInputComponent
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

    async updateNonplayed() {
      this.isLoading = true;
      await this.manager.updateNonplayed();
      this.isLoading = false;
    },

    async updatePlayed() {
      this.isLoading = true;
      await this.manager.updatePlayed();
      this.isLoading = false;
    },

    async addMap() {
      this.isLoading = true;
      await this.manager.addMap(this.inputText);
      this.isLoading = false;
    },

    async removeMap(workshop_id) {
      this.isLoading = true;
      await this.manager.removeMap(workshop_id);
      this.isLoading = false;
    }
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
    <loading v-model:active="isLoading" :is-full-page="fullPage"/>
    <div class="background">
      <div class="blur">
        <ReelComponent :ITEMS_PER_PAGE="ITEMS_PER_PAGE" :manager="manager"/>
        <MapInputComponent :manager="manager"/>
      </div>
    </div>
  </body>

  <footer>
    <p>{{ COPYRIGHT }}</p>
  </footer>
</template>