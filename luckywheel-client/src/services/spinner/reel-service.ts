import {useSpinnerStore} from "@/stores/spinner.ts";

async function buildReel(length: number) {
  const spinnerStore = useSpinnerStore();
  if(spinnerStore.selectedPlaylist) {
    return spinnerStore.selectedPlaylist;
  }
  spinnerStore.watchSelectedPlaylist().then(playlist => {

  })
}


