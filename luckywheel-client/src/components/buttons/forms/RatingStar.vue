<script setup lang="ts">
import {computed} from "vue";

const props = defineProps<{
  isFilled: boolean,
}>()

const emits = defineEmits<{
  clicked: []
}>();

const starClasses = computed(() => ({
  'fa-regular': !props.isFilled,
  'fa': props.isFilled,
}));
</script>

<template>
  <button @click="emits('clicked')">
    <i class="fa-star fa-2x"
       :class="starClasses"/>
  </button>
</template>

<style scoped>
button {
  margin: 0;
  padding: 0;
}
</style>
