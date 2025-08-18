<script setup lang="ts">
import ConfirmButton from '@/components/buttons/ConfirmButton.vue';
import RegButton from '@/components/buttons/RegButton.vue';
import { useLoginStore } from '@/stores/login';
import { ref } from 'vue';
import type {UserCredentials} from "@/models/user-credentials.ts";

const props = defineProps<{
  submitButtonText: string,
  secondaryButtonText?: string,
  showCriteria?: boolean,
}>();
const emit = defineEmits<{
  submit: [UserCredentials]
}>();

const email = ref('');
const password = ref('');

const loginStore = useLoginStore();

function submitUser() {
  emit('submit', { email: email.value, password: password.value });
}

function secondaryClicked() {
  loginStore.toggleLoggingIn();
}

</script>
<template>
  <div class="flex-column no-gap">
    <input id="email-input" type="email" v-model="email" placeholder="Enter email..." required />
    <input :class="{ invalid: !loginStore.isPasswordValid }" id="password-input" type="password" aria-describedby="password-criteria" v-model="password" placeholder="Enter password..." required />
    <p v-if="props.showCriteria" class="no-margin" id="password-criteria">
      The password must be at least 6 characters and contain uppercase and lowercase letters, digits, and alphanumeric characters.
    </p>
  </div>
  <div class="flex button-row">
    <RegButton v-if="props.secondaryButtonText" @clicked="secondaryClicked()">
      {{ props.secondaryButtonText }}
    </RegButton>
    <ConfirmButton class="submit-button" type="submit" @clicked="submitUser()">
      {{ props.submitButtonText }}
    </ConfirmButton>
  </div>
</template>
<style scoped>
.button-row {
  flex-direction: row-reverse;
}

#email-input {
  margin-bottom: 16px;
}

#password-criteria {
  font-size: small;
}

.invalid {
  color: red;
  border: red solid 2px;
}
</style>
