<template>
  <input type="button" :value="value">
</template>

<script>
export default {
  props: {
    value: {
      type: String,
      required: false
    }
  }
}
</script>

<style scoped>

input {
  padding: 0 20px;
  padding: 0 clamp(10px, 2vw, 20px);
  height: clamp(30px, 10vw, 48px);

  background: rgba(0, 0, 0, 0.18);
  border: 1px solid #6D6D6D;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
  border-radius: 10px;

  font-family: 'RobotoThin';
  font-weight: 100;
  font-style: normal;
  font-weight: 100;
  font-size: clamp(16px, 4vw, 28px);
  line-height: 28px;

  color: rgba(255, 255, 255, 0.77);
  transition-duration: 0.2s;
}

input:hover {

}

input:active {
  box-shadow: 0px 0px 4px rgba(0, 0, 0, 0.25);
  transform: translateY(2px);
  border: 1px solid #FFFFFF;
}

@media (hover: hover) {
  input:hover {
    border: 1px solid #FFFFFF;
  }
}

</style>