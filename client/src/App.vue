<template>
  <!--  <img alt="Vue logo" src="./assets/logo.png">-->
  <!--  <HelloWorld msg="Welcome to Your Vue.js App"/>-->
  <div class="app">
    <router-view></router-view>
  </div>
</template>

<script>

export default {
  name: 'App',
}
</script>

<style>
@font-face {
  font-family: 'RobotoThin';
  src: url("assets/fonts/Roboto-Thin.ttf");
  font-weight: normal;
  font-style: normal;
}
@font-face {
  font-family: 'RobotoLight';
  src: url("assets/fonts/Roboto-Light.ttf");
  font-weight: normal;
  font-style: normal;
}


#app {
  background: linear-gradient(107.56deg, #45535A 41.15%, #445542 100%);
  height: 100vh;
  width: 100vw;
}

.app{
  height: 100%;
  width: 100%;
}

*{
  margin: 0;
  padding: 0;
}

.app{

}
</style>
