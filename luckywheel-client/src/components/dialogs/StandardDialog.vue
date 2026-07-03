<script setup lang="ts">
import {useLoadingStore} from "@/stores/loading.store.ts";
import {DialogWidths} from "@/models/dialog-widths.ts";

const loadingStore = useLoadingStore();

const props = withDefaults(defineProps<{
  headerText: string,
  width?: DialogWidths,
}>(), {
  width: DialogWidths.DIALOG_600
});

</script>
<template>
  <div class="dialog" :class="props.width" v-if="!loadingStore.isLoading">
    <div class="header-box">
      <h1>{{ props.headerText }}</h1>
    </div>
    <div class="content">
      <slot></slot>
    </div>
  </div>
</template>

<style scoped>
.dialog {
  background-color: #2d2d2d;
}

.header-box {
  background-color: #262626;

  h1 {
    margin: 0;
    padding: 20px 40px;
  }
}

.content {
  padding: 20px 40px;
}
</style>
