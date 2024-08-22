<script>
import ServerManager from './ServerManager';
import MapComponent from './MapComponent.vue';
import { ref } from 'vue';

//TODO: Add functionality to purge maps from database and add proper colors to
//signify rarity
export default {
  data () {
    return {
      inputText: ref(""),
      selected_pool: ref("nonplayed"),
      rarity_percents: [0.0026, 0.0064, 0.032, 0.1598, 0.8],
      rarity_colors: ["#E4AE33","#EB4B4B", "#D32CE6", "#8847FF", "#4B69FF"]
    }
  },
  emits: ['onLoading', 'onFinishedLoading'],
  components: {
    MapComponent
  },
  methods: {
    async addMap(input) {
      return this.manager.addMap(input)
    },

    async addInputMap() {
      this.$emit("onLoading");
      return this.addMap(this.inputText)
      .then(() => {
        this.inputText = "";
        this.$emit("onFinishedLoading");
      });
    },

    async toggleMap(id) {
      this.$emit("onLoading");
      if(this.selected_pool === "played") {
        return this.manager.unplayMap(id)
          .then(() => this.$emit("onFinishedLoading"));
      } else {
        return this.manager.playMap(id)
          .then(() => this.$emit("onFinishedLoading"));
      }
    },

    async deleteMap(id) {
      this.$emit("onLoading");
      return this.manager.deleteMap(id)
        .then(() => this.$emit("onFinishedLoading"));
    },

    async startMap(id) {
      this.$emit("onLoading");
      return this.manager.startMap(id)
        .then(() => this.$emit("onFinishedLoading"));
    },

    getColor(weight) {
      if(this.selected_pool === "played") return '#ccba7c';
      const ratio = (weight + 0.0) / this.manager.totalweight;
      var acc_percentage = 0;
      for (let i = 0; i < this.rarity_percents.length; i++) {
        acc_percentage += this.rarity_percents[i];

        if(acc_percentage >= ratio) return this.rarity_colors[i];
      }
      console.log('here!!')
      return this.rarity_colors[this.rarity_colors.length];
    },

    getMapList() {
      switch(this.selected_pool) {
        case "nonplayed":
          return this.manager.nonplayed;
        case "played":
          return this.manager.played;
        default:
          return [];
      }
    }
  },
  props: {
    manager: ServerManager
  }, 
  async mounted() {
    this.$emit("onLoading");
    return this.manager.updateNonplayed()
      .then(() => this.manager.updatePlayed())
      .then(() => this.$emit("onFinishedLoading"));
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
      <button @click="addInputMap">Submit Map</button>
    </div>
  </div>
  <select v-model="selected_pool">
    <option value="nonplayed">Non-Played</option>
    <option value="played">Played</option>
  </select>
  <div class="map-container">
    <MapComponent v-for="map in getMapList()" :key="map.id" :name_="map.name"
    :image_url="map.image_url" :weight="map.weight" :id="map.id"
    :played="this.selected_pool == 'played'" :active_color="getColor(map.weight)"
    @mapRemoved="(id) => deleteMap(id)" @mapToggle="(id) => toggleMap(id)"
    @startMap="(id) => startMap(id)"/>
  </div>
</template>