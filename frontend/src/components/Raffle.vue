<script setup lang="ts">
import { ref } from 'vue';

const ITEMS_PER_PAGE = 5;
const COPYRIGHT = "© All images from Counter-Strike 2 and the Steam Workshop are property of Valve Corporation. This website is not affiliated with Valve Corporation."

var toggle = true
const text = ref('Hello, Vue 3!')

const handleClick = () => {
  toggle = !toggle
  text.value = toggle ? 'Hello, Vue 3!' : 'Hello, Vite!'
}
</script>
<style>
  html, body {
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

  .map-input > * {
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

  .reel > * {
    flex: 0 0 268px;
    background-image: url("https://cdn.cloudflare.steamstatic.com/steam/apps/730/header.jpg?t=1698860631");
    background-size: 100%;
    height: 100%;
    margin: 0;
    color: black;
  }
  
  @keyframes moveleft {
    0% { transform: translateX(0); }
    100% { transform: translateX(-2000px); }
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
          <input type="text" placeholder="Insert Steam Workshop Link..." />
          <input type="button" value="Submit" />
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