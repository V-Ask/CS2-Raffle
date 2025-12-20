<script setup lang="ts">

const props = defineProps<{
  isLoading: boolean,
}>();
</script>

<template>
  <div class="loading" v-if="isLoading">
    <p>CURRENTLY LOADING</p>
  </div>
  <div class="not-loading" v-else>
    <slot></slot>
  </div>
</template>

<style scoped>
.not-loading {
  width: 100%;
  height: 100%;
  display: flex;
}
</style>
