<script setup lang="ts">
import ConfirmButton from '@/components/buttons/ConfirmButton.vue';
import RegButton from '@/components/buttons/RegButton.vue';
import { useLoginStore } from '@/stores/login';
import { ref } from 'vue';

const props = defineProps<{
  submitButtonText: string,
  secondaryButtonText?: string
}>();
const emit = defineEmits<{
  submit: [{ username: string, password: string }]
}>();

const username = ref('');
const password = ref('');

const loginStore = useLoginStore();

function submitUser() {
  emit('submit', { username: username.value, password: password.value });
  console.log({ username: username.value, password: password.value });

}

function secondaryClicked() {
  loginStore.toggleLoggingIn();
}

</script>
<template>
  <div class="flex-column">
    <input id="username-input" type="text" v-model="username" placeholder="Enter username..." required />
    <input id="password-input" type="password" v-model="password" placeholder="Enter password..." required />
  </div>
  <div class="flex button-row">
    <RegButton v-if="props.secondaryButtonText" @clicked="secondaryClicked()">
      {{ props.secondaryButtonText }}
    </RegButton>
    <ConfirmButton class="submit-button" @clicked="submitUser()">
      {{ props.submitButtonText }}
    </ConfirmButton>
  </div>
</template>
<style scoped>
.button-row {
  flex-direction: row-reverse;
}
</style>
