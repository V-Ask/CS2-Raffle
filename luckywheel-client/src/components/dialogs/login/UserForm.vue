<script setup lang="ts">
import ConfirmButton from '@/components/buttons/ConfirmButton.vue';
import RegButton from '@/components/buttons/RegButton.vue';
import {ref} from 'vue';
import type {UserCredentials} from "@/models/user-credentials.ts";
import {useLoginStore} from "@/stores/login.store.ts";

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
  emit('submit', {email: email.value, password: password.value});
}

function secondaryClicked() {
  loginStore.toggleLoggingIn();
}

</script>
<template>
  <form class="flex-column no-gap">
    <input id="email-input" type="email" v-model="email" placeholder="Enter email..."
           autocomplete="username" required/>
    <input id="password-input" type="password" aria-describedby="password-criteria"
           v-model="password" autocomplete="current-password" placeholder="Enter password..."
           required/>
    <p class="no-margin" :class="{hidden: !props.showCriteria}" id="password-criteria">
      The password must be at least 6 characters and contain uppercase and lowercase letters,
      digits, and alphanumeric characters.
    </p>
  </form>
  <div class="flex button-row">
    <RegButton v-if="props.secondaryButtonText" class="secondary-button" @clicked="secondaryClicked()">
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

.hidden {
  opacity: 0;
}

#email-input {
  margin-bottom: 16px;
}

#password-criteria {
  font-size: small;
}

.submit-button {
  width: 20%;
}

.secondary-button {
  width: 50%;
}

.invalid {
  color: red;
  border: red solid 2px;
}
</style>
