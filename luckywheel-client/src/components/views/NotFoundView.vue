<script setup lang="ts">
import {useRoute, useRouter} from "vue-router";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import {PLAYLIST_VIEW} from "@/helpers/constants/routing.ts";

const router = useRouter();
const route = useRoute();
const path = route.params.path;

function navigateToHomepage() {
  router.push({
    name: PLAYLIST_VIEW
  })
}
</script>

<template>
  <div class="flex-column">
    <p>Error 404: Route "{{ route.path }}" not found</p>
    <ConfirmButton @clicked="navigateToHomepage()">Return to homepage?</ConfirmButton>
  </div>

</template>

<style scoped>

</style>
