<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import RatingStars from "@/components/buttons/forms/RatingStars.vue";
import {computed, onBeforeUnmount, ref} from "vue";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";

const selectedPlaylistStore = useSelectedPlaylistStore();

const props = defineProps<{
  map: ReelMap
}>();

const emits = defineEmits([
  'close'
]);

const rating = ref<{ ratingSet: boolean, rating: number }>({
  ratingSet: false,
  rating: 0
});

// Played state is only committed to the server when the dialog closes
const hasPlayed = ref<boolean>(props.map.hasPlayed);

const mapRating = computed(() => props.map.rating);

function onRated(newRating: number) {
  const shouldChangeRating = newRating !== mapRating.value;
  rating.value = {ratingSet: shouldChangeRating, rating: newRating};
}

function togglePlayed() {
  hasPlayed.value = !hasPlayed.value;
}

// The dialog can also be closed via backdrop click or Escape, which unmounts it without
// pressing "Close", so pending changes are committed on unmount
onBeforeUnmount(saveChanges);

async function saveChanges() {
  try {
    if (rating.value.ratingSet) {
      await selectedPlaylistStore.rateMap(props.map, rating.value.rating);
    }
    if (hasPlayed.value !== props.map.hasPlayed) {
      await selectedPlaylistStore.markMapAsPlayed(props.map, hasPlayed.value);
    }
  } catch (e) {
    console.error(e);
  }
}

function close() {
  emits('close');
}
</script>

<template>
  <StandardDialog header-text="Edit map" :use-footer="true">
    <template v-slot:content>
      <div class="form-wrapper">
        <h1>Information</h1>
        <div class="info-text">
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
        </div>
      </div>
      <div class="edit-row">
        <RatingStars @rated="onRated" :starting-rating="map.rating ?? 0"/>
        <RegButton :class="{played: hasPlayed}"
                   @clicked="togglePlayed">
          {{ hasPlayed ? 'Mark as unplayed' : 'Mark as played' }}
        </RegButton>
      </div>
    </template>
    <template v-slot:footer-content>
      <div class="footer">
        <RegButton @clicked="close">Close</RegButton>
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

.form-wrapper {
  display: flex;
  flex-direction: column;
}

.info-row {
  flex-grow: 0;
  display: grid;
  grid-template-columns: auto minmax(0, 1fr);
  gap: 10rem;

  .row-title {
    font-weight: bold;
    white-space: nowrap;
  }

  .row-value {
    color: gray;
    text-align: right;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }
}

.info-row:last-child {
  p {
    margin-bottom: 0;
  }
}

.info-text {
  margin-bottom: 1rem;
}

.edit-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 1rem;

  .played {
    background-color: green;
  }
}

.footer {
  display: flex;
  justify-content: flex-end;
}
</style>
