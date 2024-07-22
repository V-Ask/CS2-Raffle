<script>
import ServerManager from './ServerManager';
import { ref } from 'vue';


export default {
  data () {
    return {
      inputText: ref(""),
      selected_pool: ref(""),
    }
  },
  methods: {
    async addMap() {
      this.$emit("onLoading");
      this.manager.addMap(this.inputText);
      this.$emit("onFinishedLoading");
    },

    getMapList() {
      switch(this.selected_pool) {
        case "nonplayed":
          return this.manager.nonplayed;
        case "played":
          return this.manager.played;
        default:
          console.log(this.selected_pool);
          return [];
      }
    }
  },
  props: {
    manager: ServerManager
  }, 
  beforeMount() {
    this.$emit("onLoading");
    this.manager.updateNonplayed();
    this.$emit("onFinishedLoading");
  }
}

</script>

<style>
.input-container {
  width: 40vw;
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

.map-container {
  display: flex;
  justify-content: center;
  align-items: flex-start;
  height: 30vh;
  width: calc(v-bind(ITEMS_PER_PAGE) * 10%);
  overflow: auto;
  flex-wrap: wrap;
}

.map-container >* {
  flex: 0 0 1;
  margin: 2px;
  height: 200px;
}

::-webkit-scrollbar {
  width: 1em;
}

::-webkit-scrollbar-track {
  box-shadow: inset 0 0 50px rgba(0, 0, 0, 0.3);
}

::-webkit-scrollbar-thumb {
  background-color: rgb(255, 94, 0);
}

@keyframes shake {
  0% {
    margin-left: 0rem;
  }
  25% {
    margin-left: 0.5rem;
  }
  75% {
    margin-left: -0.5rem;
  }
  100% {
    margin-left: 0rem;
  }
}
</style>

<template>
<div class="input-container">
  <span>Add Map to Pool:</span>
  <div class="map-input">
    <input v-model="inputText" placeholder="Insert Steam Workshop Link..." />
    <button @click="addMap">Submit Map</button>
  </div>
</div>
<select v-model="selected_pool">
  <option value="nonplayed">Non-Played</option>
  <option value="played">Played</option>
</select>
<div class="map-container">
  <img v-for="map in getMapList()" :key="map.id"
  v-on:click="manager.removeMap(map.id)"  :src="map.image_url"/>
</div>
</template>