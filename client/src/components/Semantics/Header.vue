<template>
  <div class="Header">
    <div class="menu">
      <div class="line"></div>
      <div class="line"></div>
      <div class="line"></div>
    </div>
    <div class="info">

      <div class="email">Salov.tem@bk.ru</div>
      <div class="avatar" @mouseover="menuB=true" @mouseout="menuB=false">
        <img :src="img" alt="">
        <!--        <transition name="modalB">-->
        <div class="menuB">
          <div @click="logout" class="logout">Выйти</div>
        </div>
        <!--        </transition>-->
      </div>
    </div>
  </div>
</template>

<script>
import logo from "@/assets/logo.png";
import UserService from "@/services/UserService";
import router from "@/router/router";

export default {
  name: "HeaderTop",
  components: {},
  data() {
    return {
      img: logo,
      menuB: false,
    }
  },
  methods:
      {
       async logout() {
          if(!(await UserService.Logout())){
            alert("Не удалось произвести выход, потеряна связь с сервером");
            return;
          }

          localStorage.clear();
          router.push("/");
        }

      },


}
</script>

<style scoped>

.menuB {
  /*display: none;*/
  opacity: 0;
  transition-duration: 1s;
  position: relative;
  /*left: -100px;*/
  right: 90px;
  top: 10px;
  background: #4B4F51;
  border: 0.1px solid #FFFFFF;
  border-radius: 2px;
  min-height: 100px;
  min-width: 130px;
}

:root {
  --margin-h: clamp(35px, 4vw, 50px)
}

.modalB-enter-active, .modalB-leave-active, .modalB-move {
  transition: opacity 0.5s;
}

.modalB-enter, .modalB-leave-to {
  opacity: 0;
}

.Header {
  position: fixed;
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 60px;
  background: #484C4D;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
  width: 100%;
  z-index: 9999;
}

.info {
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: 100%;
}

.avatar {
  width: 35px;
  height: 35px;
  margin: 0 clamp(35px, 4vw, 50px);
  transition-duration: 1s;
}

.avatar:hover .menuB {
  /*display: block;*/
  transition-duration: 1s;
  opacity: 1;
}

.avatar:hover {
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
}

.avatar img {
  width: 100%;
  height: 100%;
  border-radius: 50%;
}


.menu {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  height: 30px;

  margin: 0 clamp(35px, 4vw, 50px);

  opacity: 0;
}

.line {
  width: 42px;
  height: 6px;

  background: #D9D9D9;
  border-radius: 10px;
}

.email {
  font-family: 'RobotoLight';
  font-weight: 100;
  font-style: normal;
  font-weight: 100;
  font-size: clamp(16px, 4vw, 24px);
  line-height: 28px;
  color: #FFFFFF;
}

</style>