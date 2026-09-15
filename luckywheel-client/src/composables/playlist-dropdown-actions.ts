import {computed, onMounted, ref, watch} from "vue";
import type {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";
import {usePlaylistStore} from "@/stores/spinner.store.ts";

export function usePlaylistDropdownActions() {
  const playlistStore = usePlaylistStore();
  const selectedPlaylistStore = useSelectedPlaylistStore();

  const playlistIndices = ref<WorkshopPlaylistIndex[]>([]);
  const selectedPlaylistIndex = ref<WorkshopPlaylistIndex | null>(null);
  const playlistIndexOptionFn = computed(() => (index: WorkshopPlaylistIndex) => index.playlistId);
  const playlistIndexTextFn = computed(() => (index: WorkshopPlaylistIndex) => index.collectionName);

  watch(selectedPlaylistIndex, (selectedPlaylistIndex) => {
    if(selectedPlaylistIndex) {
      selectedPlaylistStore.selectPlaylistByIndex(selectedPlaylistIndex).then();
    }
  })

  onMounted(async () => {
    playlistIndices.value = await playlistStore.ensurePlaylistIndices();
  });

  return {
    playlistIndices,
    selectedPlaylistIndex,
    playlistIndexOptionFn,
    playlistIndexTextFn
  }
}
