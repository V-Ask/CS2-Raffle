<script setup lang="ts">
import {useLoadingStore} from "@/stores/loading.store.ts";

const loadingStore = useLoadingStore();

const props = defineProps<{
  headerText: string
}>();

</script>
<template>
  <div class="dialog" v-if="!loadingStore.isLoading">
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
  min-width: 200px;
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
