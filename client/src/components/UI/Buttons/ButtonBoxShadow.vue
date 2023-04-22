<template>

  <button class="button-create-list">
    <slot></slot>
    <div class="icon">
      <svg width="29" height="34" viewBox="0 0 29 34" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M0 0H15.6111L28.6203 8.36364V16.7273V33.4545H0V0Z" fill="#D9D9D9"/>
        <path d="M2.60156 2.09091H15.3743L26.0182 9.40909V16.7273V31.3636H2.60156V2.09091Z" fill="#494D4F"/>
        <rect x="5.20361" y="8.36363" width="13.0092" height="2.09091" rx="1" fill="#D9D9D9"/>
        <rect x="5.20361" y="12.5455" width="18.2129" height="2.09091" rx="1" fill="#D9D9D9"/>
        <rect x="5.20361" y="16.7273" width="18.2129" height="2.09091" rx="1" fill="#D9D9D9"/>
        <rect x="5.20361" y="20.9091" width="18.2129" height="2.09091" rx="1" fill="#D9D9D9"/>
        <rect x="5.20361" y="25.0909" width="18.2129" height="2.09091" rx="1" fill="#D9D9D9"/>
      </svg>
    </div>
    <div class="text">
      Добавить страницу
    </div>
  </button>
  <!--  <input type="button" :value="value">-->
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

.createList{
  margin-top: 120px;
  margin-bottom: 10px;
  width: 100%;
  display: flex;
  justify-content: center;
  /*align-items: center;*/
}

.button-create-list{
  border: 0;
  display: flex;
  align-items: center;
  height: 65px;
  padding: 5px 40px;
  user-select: none;

  background: rgba(74, 74, 74, 0.71);
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
  border-radius: 10px;

  font-family: 'Roboto Light';
  font-style: normal;
  font-weight: 300;
  font-size: 24px;
  line-height: 28px;

  color: #FFFFFF;

  transition-duration: 0.3s;
}

.icon{
  margin-right: 30px;
}

.button-create-list:hover{
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.5);
}

.button-create-list:active{
  transform: translateY(2px);
  box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.5);
}

</style>