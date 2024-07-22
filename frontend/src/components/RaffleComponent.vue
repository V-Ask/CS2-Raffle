<script>
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css'
import ReelComponent from './ReelComponent.vue';
import ServerManager from './ServerManager'
import MapInputComponent from './MapInputComponent.vue';

//TODO: Fix loading
//Note: https://michaelnthiessen.com/pass-function-as-prop
export default {
  data() {
    return {
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
    setLoading(isLoading) {
      this.isLoading = isLoading;
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
        <ReelComponent @onLoading="setLoading(true)" @onFinishedLoading="setLoading(false)" :ITEMS_PER_PAGE="ITEMS_PER_PAGE" :manager="manager"/>
        <MapInputComponent @onLoading="setLoading(true)" @onFinishedLoading="setLoading(false)" :manager="manager"/>
      </div>
    </div>
  </body>

  <footer>
    <p>{{ COPYRIGHT }}</p>
  </footer>
</template>