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

input {
  background: rgba(217, 217, 217, 0.08);
  border: 1px solid #FFFFFF;
  border-radius: 10px;

  font-family: 'RobotoThin';
  font-weight: 100;
  font-style: normal;
  font-weight: 100;
  font-size: clamp(12px, 4vw, 28px);
  line-height: 28px;

  color: #FFFFFF;

  padding: clamp(2px, 1vw, 8px) 16px;

  /*width: 100%;*/

  transition-duration: 0.5s;
}

input::placeholder {
  font-family: 'RobotoThin';
  font-style: normal;
  font-weight: 100;
  font-size: clamp(12px, 4vw, 28px);
  line-height: 28px;

  color: rgba(255, 255, 255, 0.77);
  transition-duration: 0.35s;
}

input:focus {
  outline: none;
  background: #494D4D;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
}

input:focus::placeholder {
  outline: none;
  color: rgba(255, 255, 255, 0.5);
}

</style>