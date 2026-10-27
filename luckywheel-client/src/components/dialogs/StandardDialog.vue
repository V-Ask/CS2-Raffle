<script setup lang="ts">
import {DialogWidths} from "@/models/dialog-widths.ts";

const props = withDefaults(defineProps<{
  headerText: string,
  width?: DialogWidths,
  useFooter?: boolean,
}>(), {
  width: DialogWidths.DIALOG_600
});

</script>
<template>
  <div class="dialog" :class="props.width">
    <div class="header-box">
      <h1>{{ props.headerText }}</h1>
    </div>
    <div class="content">
      <slot name="content"></slot>
    </div>
    <div class="footer" v-if="props.useFooter">
      <slot name="footer-content"></slot>
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

.footer {
  padding: 20px 40px;
}
</style>
