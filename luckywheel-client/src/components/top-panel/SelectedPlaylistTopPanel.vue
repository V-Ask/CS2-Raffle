<script setup lang="ts">

import {InputTypes} from "@/models/input-types.ts";
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import SortingOptionsButton from "@/components/top-panel/SortingOptionsButton.vue";
import {useSortingOptionsStore} from "@/stores/sorting-options.store.ts";

const sortingOptionsStore = useSortingOptionsStore();

const searchText = defineModel<string>();

</script>

<template>
  <div class="panel">
    <p>
      Showing {{ sortingOptionsStore.showStateLabel }}. Sorted by {{ sortingOptionsStore.sortingOptionLabel }}
    </p>
    <SortingOptionsButton/>
    <SingleLineTextField
      v-model="searchText"
      placeholder="Search maps..."
      :input-type="InputTypes.SEARCH"
    />
  </div>
</template>

<style scoped>
.panel {
  display: flex;
  gap: 1rem;
}
</style>
