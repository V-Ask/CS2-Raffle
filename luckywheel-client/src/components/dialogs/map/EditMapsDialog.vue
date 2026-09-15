<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import RatingStars from "@/components/buttons/forms/RatingStars.vue";

const props = defineProps<{
  map: ReelMap
}>();

const emits = defineEmits([
  'close'
]);
</script>

<template>
  <StandardDialog header-text="Edit map" :use-footer="true">
    <template v-slot:content>
      <div class="form-wrapper">
        <h1>Information</h1>
        <div>
          <div class="info-row">
            <p class="row-title">Name:</p>
            <p class="row-value">{{ props.map.mapName }}</p>
          </div>
          <div class="info-row">
            <p class="row-title">Thumbnail URL:</p>
            <p class="row-value">{{ props.map.imageUrl }}</p>
          </div>
          <div class="info-row">
            <p class="row-title">ID:</p>
            <p class="row-value">{{ props.map.workshopId }}</p>
          </div>
          <div class="info-row">
            <p class="row-title">Weight:</p>
            <p class="row-value">{{ props.map.weight }}</p>
          </div>
          <div class="info-row">
            <p class="row-title">Description:</p>
            <p class="row-value">{{ props.map.description }}</p>
          </div>
          <div>
            <RatingStars />
          </div>
        </div>
      </div>
    </template>
    <template v-slot:footer-content>
      <div class="footer">
        <RegButton @click="emits('close')">Close</RegButton>
      </div>
    </template>
  </StandardDialog>
</template>

<style scoped>
h1 {
  margin-top: 0;
  margin-bottom: 0.5em;
  font-size: 1.5em;
}

p {
  margin: 0.5em 0;
}

.divider {
  margin: 1em 0;
}

.form-wrapper {
  display: flex;
  flex-direction: column;
}

.info-row {
  display: grid;
  grid-auto-flow: column;
  justify-content: space-between;

  .row-title {
    font-weight: bold;
  }

  .row-value {
    color: gray;
  }
}

.info-row:last-child {
  p {
    margin-bottom: 0;
  }
}

.footer {
  display: flex;
  justify-content: flex-end;
}
</style>
