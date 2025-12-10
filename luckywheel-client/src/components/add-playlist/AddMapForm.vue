<script setup lang="ts">
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {ref} from "vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import RoutingService from "@/services/routing.service.ts";
import WorkshopLinkService from "@/services/workshop/workshop-link.service.ts";

interface Props {
  playlistId: string;
}

const props = defineProps<Props>();

const spinnerStore = useSpinnerStore();
const workshopUrl = ref("");

function returnToPlaylistView() {
  RoutingService.navigateToPlaylistPage(props.playlistId);
}

// TODO: This need validation
function addNewMap() {
  let id = WorkshopLinkService.getWorkshopId(workshopUrl.value);
  if(!id) {
    console.warn("Invalid Workshop Link -- this needs clearer error reporting");
    return;
  }
  spinnerStore.addWorkshopMapIdToSelectedPlaylist(id).then(() => {
    returnToPlaylistView();
  })
}
</script>

<template>
  <SingleLineTextField placeholder="Insert Workshop URL here" v-model="workshopUrl"></SingleLineTextField>
  <ConfirmButton @click="addNewMap()">Add</ConfirmButton>
  <RegButton @click="returnToPlaylistView()">Cancel</RegButton>
</template>

<style scoped>

</style>
