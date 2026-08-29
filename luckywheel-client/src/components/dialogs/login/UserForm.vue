<script setup lang="ts">
import RegButton from '@/components/buttons/RegButton.vue';
import {ref} from 'vue';
import type {UserCredentials} from "@/models/user-credentials.ts";
import {useLoginStore} from "@/stores/login.store.ts";
import ConfirmSubmitButton from "@/components/buttons/forms/ConfirmSubmitButton.vue";

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

function submitUser(event: SubmitEvent) {
  // By default, submission would reload the page
  event.preventDefault();
  emit('submit', {email: email.value, password: password.value});
}

function secondaryClicked() {
  loginStore.toggleLoggingIn();
}

</script>
<template>
  <form @submit="submitUser($event)">
    <div class="flex-column no-gap">
      <input id="email-input" type="email" v-model="email" placeholder="Enter email..."
             autocomplete="username" required/>
      <input id="password-input" type="password" aria-describedby="password-criteria"
             v-model="password" autocomplete="current-password" placeholder="Enter password..."
             required/>
      <p class="no-margin" :class="{hidden: !props.showCriteria}" id="password-criteria">
        The password must be at least 6 characters and contain uppercase and lowercase letters,
        digits, and alphanumeric characters.
      </p>
    </div>
    <div class="flex button-row">

      <ConfirmSubmitButton class="submit-button">
        {{ props.submitButtonText }}
      </ConfirmSubmitButton>
      <RegButton v-if="props.secondaryButtonText" class="secondary-button"
                 @clicked="secondaryClicked()">
        {{ props.secondaryButtonText }}
      </RegButton>
    </div>
  </form>
</template>
<style scoped>
.button-row {
  justify-content: flex-end;
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
