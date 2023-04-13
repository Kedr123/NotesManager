<template>
  <div class="auth-page">
    <AuthForm/>
  </div>
</template>

<script>
import AuthForm from "@/components/Forms/AuthForm";

export default {
  name:'AuthPage',
  components: {
    AuthForm,
  }
}
</script>

<style scoped>

.auth-page{
  width: 100%;
  height: 100%;

  display: flex;
  align-items: center;
  justify-content: center;
}

</style>