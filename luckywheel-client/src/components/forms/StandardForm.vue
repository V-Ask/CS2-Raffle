<script setup lang="ts">
import { ref, computed, watch } from 'vue'

interface FormField {
  key: string
  label: string
  type: 'text' | 'email' | 'password' | 'number' | 'textarea' | 'select' | 'checkbox'
  placeholder?: string
  required?: boolean
  options?: { value: string; label: string }[]
  validation?: (value: any) => string | null
}

interface Props {
  fields: FormField[]
  submitText?: string
  resetText?: string
  showReset?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  submitText: 'Submit',
  resetText: 'Reset',
  showReset: true
})

const emit = defineEmits<{
  submit: [data: Record<string, any>]
  change: [data: Record<string, any>]
}>()

const formData = ref<Record<string, any>>({})
const errors = ref<Record<string, string>>({})

const initializeFormData = () => {
  const data: Record<string, any> = {}
  props.fields.forEach(field => {
    data[field.key] = field.type === 'checkbox' ? false : ''
  })
  formData.value = data
}

initializeFormData()

watch(() => props.fields, initializeFormData, { deep: true })

watch(formData, (newData) => {
  emit('change', { ...newData })
}, { deep: true })

const validateField = (field: FormField, value: any): string | null => {
  if (field.required && (!value || (typeof value === 'string' && !value.trim()))) {
    return `${field.label} is required`
  }

  if (field.validation) {
    return field.validation(value)
  }

  return null
}

const validateForm = (): boolean => {
  const newErrors: Record<string, string> = {}

  props.fields.forEach(field => {
    const error = validateField(field, formData.value[field.key])
    if (error) {
      newErrors[field.key] = error
    }
  })

  errors.value = newErrors
  return Object.keys(newErrors).length === 0
}

const handleSubmit = () => {
  if (validateForm()) {
    emit('submit', { ...formData.value })
  }
}

const handleReset = () => {
  initializeFormData()
  errors.value = {}
}

const handleBlur = (field: FormField) => {
  const error = validateField(field, formData.value[field.key])
  if (error) {
    errors.value[field.key] = error
  } else {
    delete errors.value[field.key]
  }
}
</script>

<template>
  <form @submit.prevent="handleSubmit" class="standard-form">
    <div v-for="field in fields" :key="field.key" class="form-group">
      <label :for="field.key" class="form-label">
        {{ field.label }}
        <span v-if="field.required" class="required">*</span>
      </label>

      <input
        v-if="field.type === 'text' || field.type === 'email' || field.type === 'password' || field.type === 'number'"
        :id="field.key"
        v-model="formData[field.key]"
        :type="field.type"
        :placeholder="field.placeholder"
        :class="['form-input', { 'error': errors[field.key] }]"
        @blur="handleBlur(field)"
      />

      <textarea
        v-else-if="field.type === 'textarea'"
        :id="field.key"
        v-model="formData[field.key]"
        :placeholder="field.placeholder"
        :class="['form-textarea', { 'error': errors[field.key] }]"
        @blur="handleBlur(field)"
        rows="4"
      />

      <select
        v-else-if="field.type === 'select'"
        :id="field.key"
        v-model="formData[field.key]"
        :class="['form-select', { 'error': errors[field.key] }]"
        @blur="handleBlur(field)"
      >
        <option value="">Select an option</option>
        <option
          v-for="option in field.options"
          :key="option.value"
          :value="option.value"
        >
          {{ option.label }}
        </option>
      </select>

      <label
        v-else-if="field.type === 'checkbox'"
        :for="field.key"
        class="checkbox-label"
      >
        <input
          :id="field.key"
          v-model="formData[field.key]"
          type="checkbox"
          class="form-checkbox"
          @blur="handleBlur(field)"
        />
        <span class="checkbox-text">{{ field.placeholder || field.label }}</span>
      </label>

      <div v-if="errors[field.key]" class="error-message">
        {{ errors[field.key] }}
      </div>
    </div>

    <div class="form-actions">
      <button type="submit" class="btn btn-primary">
        {{ submitText }}
      </button>
      <button
        v-if="showReset"
        type="button"
        class="btn btn-secondary"
        @click="handleReset"
      >
        {{ resetText }}
      </button>
    </div>
  </form>
</template>

<style scoped>
.standard-form {
  max-width: 500px;
  margin: 0 auto;
}

.form-group {
  margin-bottom: 1.5rem;
}

.form-label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 600;
  color: #374151;
}

.required {
  color: #ef4444;
}

.form-input,
.form-textarea,
.form-select {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #d1d5db;
  border-radius: 0.375rem;
  font-size: 1rem;
  transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}

.form-input:focus,
.form-textarea:focus,
.form-select:focus {
  outline: none;
  border-color: #3b82f6;
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
}

.form-input.error,
.form-textarea.error,
.form-select.error {
  border-color: #ef4444;
}

.checkbox-label {
  display: flex;
  align-items: center;
  cursor: pointer;
  font-weight: normal;
}

.form-checkbox {
  margin-right: 0.5rem;
  width: 1rem;
  height: 1rem;
}

.checkbox-text {
  user-select: none;
}

.error-message {
  margin-top: 0.25rem;
  font-size: 0.875rem;
  color: #ef4444;
}

.form-actions {
  display: flex;
  gap: 1rem;
  margin-top: 2rem;
}

.btn {
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 0.375rem;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.15s ease-in-out;
}

.btn-primary {
  background-color: #3b82f6;
  color: white;
}

.btn-primary:hover {
  background-color: #2563eb;
}

.btn-secondary {
  background-color: #6b7280;
  color: white;
}

.btn-secondary:hover {
  background-color: #4b5563;
}
</style>
