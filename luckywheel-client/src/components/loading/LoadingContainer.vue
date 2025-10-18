<script setup lang="ts">

const props = defineProps<{
  isLoading: boolean,
}>();
</script>

<template>
  <div class="loading" v-if="isLoading">
    <p>CURRENTLY LOADING</p>
  </div>
  <slot v-else></slot>
</template>

<style scoped>

</style>
