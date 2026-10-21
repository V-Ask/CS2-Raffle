<script setup lang="ts">
const props = defineProps<{
  rating: number
}>();
</script>

<template>
  <div class="star-summary"
        :title="`Rating ${rating}`"
  >
    <i class="fa-star fa fa-lg"></i>
    <span class="rating"

    >{{ props.rating }}</span>
  </div>
</template>

<style scoped>
.star-summary {
  position: relative;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  line-height: 1;
}

.rating {
  position: absolute;
  display: flex;
  justify-content: center;
  padding-bottom: 0.15em;
  font-size: 0.85em;
  line-height: 1;
  pointer-events: none;
  color: white;
}

.fa-star {
  color: black;
}
</style>
