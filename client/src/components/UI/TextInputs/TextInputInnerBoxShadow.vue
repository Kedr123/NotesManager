<template>
  <input :type="type" :placeholder="placeholder" :value="modelValue" @input="$emit('update:modelValue', $event.target.value)">
</template>

<script>
export default {
  props: {
    placeholder: {
      type: String,
      required: false,
    },
    type: {
      type: String,
      required: false,
    },
    modelValue: {
      type: String,
      required: false,
    },

  },
  emits: ['update:modelValue'],
  data() {
    return {
      // value: ""
    }
  }
}
</script>

<style scoped>

input{
  width: 100%;
  height: 100%;

  outline: none;
  text-align: center;

  border: 0;
  background: #44484A;
  box-shadow: inset 0px 4px 4px rgba(0, 0, 0, 0.25);
  border-radius: 10px;

  font-family: 'Roboto Light';
  font-style: normal;
  font-weight: 300;
  font-size: 24px;
  line-height: 150%;
  /* identical to box height, or 24px */

  /*text-align: justify;*/

  color: #D9D9D9;
}

</style>