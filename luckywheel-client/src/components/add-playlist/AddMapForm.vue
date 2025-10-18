<script setup lang="ts">
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {ref} from "vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import RoutingService from "@/services/routing.service.ts";

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
  spinnerStore.addMapToSelectedPlaylist(workshopUrl.value).then(() => {
    returnToPlaylistView();
  })
}
</script>

<template>
  <SingleLineTextField placeholder="Insert Workshop URL here" v-model="workshopUrl"></SingleLineTextField>
  <ConfirmButton>Add</ConfirmButton>
  <RegButton>Cancel</RegButton>
</template>

<style scoped>

</style>
