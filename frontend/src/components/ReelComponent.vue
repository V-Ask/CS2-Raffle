<script>
import axios from 'axios'
import ServerManager from './ServerManager';
import { defineComponent } from 'vue';

const REEL_SIZE = 100;
const ReelStatus = {
  Pregen: 0,
  Gen: 1,
  Rolling: 2,
  Picked: 3
}

export default defineComponent({
  data() {
    return {
      STARTING_OFFSET: 2,
      WINNER: 95,
      SPIN_TIME: 10,
      reel: [],
      reel_winner: undefined,
      reel_status: ReelStatus.Pregen,
      remove_map: true
    }
  },
  props: {
    ITEMS_PER_PAGE: Number,
    manager: ServerManager
  },
  emits: ['onLoading', 'onFinishedLoading'],
  methods: {
    async startMap() {
      const winner_id = this.reel[this.WINNER]['id']
      if(this.reel_status != 3) return; 
      const path = 'http://localhost:5000/startmap';
      this.$emit("onLoading");
      return axios.put(path, {
        workshop_id: winner_id,
        remove: this.remove_map
      })
        .then(() => this.remove_map ? this.manager.removeMap(winner_id) : Promise.resolve())
        .then(() => this.reel_status = ReelStatus.Pregen)
        .then(() => this.$emit("onFinishedLoading"))
        .catch((error) => {
          console.error(error);
        });
    },

    spin() {
      this.reel_status = ReelStatus.Rolling;
      let reel = document.getElementById('reel');
      reel.classList.toggle('rolling-reel')
      setTimeout(() => {
          this.reel_status = ReelStatus.Picked;
      }, this.SPIN_TIME * 1000);
    },

    spawnReel() {
      if(this.manager.nonplayed.length < 1) {
          alert("No Workshop maps in the playlist!");
          return;
      }
      let reel_elements = []
      this.isLoading = true;
      for (const map_id in this.manager.nonplayed) {
        const map = this.manager.nonplayed[map_id];
        var clone = Array(map.weight).fill(map);
        reel_elements.push(...clone);
      }
      let result = []
      while (result.length < REEL_SIZE) {
          const i = Math.floor(Math.random() * reel_elements.length);
          const el = reel_elements[i];
          result.push(el);
      } 
      this.reel = result;
      this.reel_winner = result[this.WINNER];
      this.isLoading = false;
      this.reel_status = ReelStatus.Gen;
    }
  }
})
</script>

<style>
.spawn-reel-overlay {
  height: 100%;
  width: 100%;
}

.spawn-reel-overlay >*{
  width: 100%;
  padding: 10%;
}

.reel-container {
  height: 100%;
  position: relative;
  width: calc(v-bind(ITEMS_PER_PAGE) * 402px);
  border: 2px solid white;
  overflow: hidden;
  display: inline-flex;
  justify-content: center;
}

#reel {
  top: 0;
  left: 0;
  position: absolute;
  height: 100%;
  display: flex;
  transition: all calc(v-bind(SPIN_TIME) * 1s) ease-in;
  transition-timing-function: cubic-bezier(0.13, 0.75, 0, 1);
}

.rolling-reel {
  margin-left: calc((v-bind(WINNER) - v-bind(STARTING_OFFSET)) * -402px);
}

#reel>* {
  flex: 0 0 402px;
  background-image: url("https://cdn.cloudflare.steamstatic.com/steam/apps/730/header.jpg?t=1698860631");
  background-size: 100%;
  height: 100%;
  margin: 0;
  color: black;
}

.reel-arrow {
  position: relative;
  background-color: rgb(127, 0, 0);
  width: 4px;
  height: 100%;
}

.reel-box {
  height: 226.5px;
}

.play-map-overlay {
  height: 226.5px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.play-map-overlay>* {
  max-width: 100%;
  max-height: 100%;
}

.play-map-buttons {
  display: flex;
  flex-direction: column;
  gap: 10px;
  margin: 6px;
}

</style>
<template>
    <div class="reel-box">
        <div class="reel-container" v-if="reel_status === 1 || reel_status === 2">
        <div id="reel">
            <img v-for="map in reel" :src="map.image_url" :key="map.workshop_id"/>
        </div>
        <div class="reel-arrow" v-if="reel_status === 1 || reel_status === 2"/>
        </div>
        <div class="spawn-reel-overlay" v-else-if="reel_status === 0">
        <button @click="spawnReel">Generate Reel...</button>
        </div>
        <div class="play-map-overlay" v-else>
          <img :src="reel_winner.image_url"/>
          <div class="play-map-buttons">
            <button @click="startMap">Start</button>
            <button @click="spawnReel">Re-gen</button>
            <label for="remove_checkbox">Remove Map?</label>
            <input type="checkbox" id="remove_checkbox" v-model="remove_map" />
          </div>
        </div>
    </div>
    <button v-if="reel_status === 1" @click="spin">Roll</button>
</template>
