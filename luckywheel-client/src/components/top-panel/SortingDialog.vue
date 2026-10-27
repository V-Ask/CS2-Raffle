<script setup lang="ts">

import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import DropdownSelector from "@/components/inputs/DropdownSelector.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import {
  MapShowStates, showStateLabels, sortOptionLabels,
  SortOptions,
  useSortingOptionsStore
} from "@/stores/sorting-options.store.ts";

const emits = defineEmits(['close']);

const sortingOptionsStore = useSortingOptionsStore();



</script>

<template>
  <StandardDialog header-text="Sorting" use-footer>
    <template v-slot:content>
      <div class="options-grid">
        <div class="options-row">
          <span>Show maps:</span>
          <DropdownSelector v-model="sortingOptionsStore.showState"
                            :options="Object.values(MapShowStates)"
                            :option-index-fn="option => option"
                            :option-text-fn="option => showStateLabels[option]">
            Show maps
          </DropdownSelector>
        </div>
        <div class="options-row">
          <span>Sort by:</span>
          <DropdownSelector v-model="sortingOptionsStore.sortingOption"
                            :options="Object.values(SortOptions)"
                            :option-index-fn="option => option"
                            :option-text-fn="option => sortOptionLabels[option]">
            Sort by
          </DropdownSelector>
        </div>
      </div>
    </template>
    <template v-slot:footer-content>
      <div class="buttons">
        <RegButton @clicked="emits('close')">Close</RegButton>
      </div>
    </template>
  </StandardDialog>
</template>

<style scoped>
.options-grid {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.options-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 2rem;
  color: #c8c8c8;
}

.options-row :deep(.selector) {
  height: 2.5rem;
  color: #c8c8c8;
}

.options-row :deep(.selector option) {
  color: #c8c8c8;
}

.buttons {
  display: flex;
  justify-content: flex-end;
}
</style>
