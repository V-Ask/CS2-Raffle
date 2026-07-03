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
  hasError?: boolean,
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

  const passwordVal = password.value;

  emit('submit', {email: email.value, password: passwordVal});
}

function secondaryClicked() {
  loginStore.toggleLoggingIn();
}

</script>
<template>
  <form @submit="submitUser($event)" class="form-wrapper">
    <div class="form-grid">
      <input id="email-input" type="email" v-model="email" placeholder="Enter email..."
             autocomplete="username" required/>
      <input id="password-input" type="password" aria-describedby="password-criteria"
             v-model="password" autocomplete="current-password" placeholder="Enter password..."
             required/>
      <Transition name="slide">
        <p v-if="showCriteria"
           class="no-margin"
           :class="{'error': hasError }"
           id="password-criteria"
        >
          The password must be at least 6 characters and contain uppercase and lowercase letters,
          digits, and alphanumeric characters.
        </p>
      </Transition>
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
#password-criteria {
  color: #989898;
}

.form-wrapper {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.form-grid {
  display: grid;
}

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

.error {
  color: red !important;
}
</style>
