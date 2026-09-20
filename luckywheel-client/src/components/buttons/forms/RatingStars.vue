<script setup lang="ts">
import {computed, ref} from "vue";
import RatingStar from "@/components/buttons/forms/RatingStar.vue";

const props = defineProps<{
  startingRating: number
}>();

const emits = defineEmits<{
  (e: 'rated', rating: number): void
}>();

const boundedStartingRating = computed(() => {
  if (props.startingRating > 5) {
    return 0;
  }
  return props.startingRating;
})
const curRating = ref(boundedStartingRating.value);
const curHover = ref(-1);

const isStarOneFilled = createStarFilledComputed(1)
const isStarTwoFilled = createStarFilledComputed(2)
const isStarThreeFilled = createStarFilledComputed(3)
const isStarFourFilled = createStarFilledComputed(4)
const isStarFiveFilled = createStarFilledComputed(5)

function setRating(newRating: number) {
  if (curRating.value === newRating) {
    curRating.value = 0;
  } else {
    curRating.value = newRating;
  }
  emits('rated', newRating);
}

function setHover(hoverRating: number) {
  curHover.value = hoverRating;
}

function removeHover() {
  curHover.value = -1;
}

function createStarFilledComputed(rating: number) {
  return computed(() => {
    if (curHover.value > 0) {
      return curHover.value >= rating;
    }
    return curRating.value >= rating;
  })
}
</script>

<template>
  <div>
    <RatingStar :is-filled="isStarOneFilled"
                @mouseover="setHover(1)"
                @mouseout="removeHover"
                @clicked="setRating(1)"
    />
    <RatingStar :is-filled="isStarTwoFilled"
                @mouseover="setHover(2)"
                @mouseout="removeHover"
                @clicked="setRating(2)"
    />
    <RatingStar :is-filled="isStarThreeFilled"
                @mouseover="setHover(3)"
                @mouseout="removeHover"
                @clicked="setRating(3)"
    />
    <RatingStar :is-filled="isStarFourFilled"
                @mouseover="setHover(4)"
                @mouseout="removeHover"
                @clicked="setRating(4)"
    />
    <RatingStar :is-filled="isStarFiveFilled"
                @mouseover="setHover(5)"
                @mouseout="removeHover"
                @clicked="setRating(5)"
    />
  </div>
</template>

<style scoped>
</style>
