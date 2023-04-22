<template>
  <div>
    <transition-group name="modalA">
      <CreateListForm @create="createList" @close="modal" v-if="isModal" class="modal"/>
      <div v-if="isModal" class="fog" @click="modal"></div>
    </transition-group>
    <ButtonBoxShadow @click="modal"/>
  </div>
</template>

<script>
import ButtonBoxShadow from "@/components/UI/Buttons/ButtonBoxShadow";
import CreateListForm from "@/components/Forms/CreateListForm";
// import ListService from "@/services/ListService";
// import UserService from "@/services/UserService";

export default {

  components: {
    CreateListForm,
    ButtonBoxShadow
  },
  data(){
    return{
      isModal:false
    }
  },
  methods:{
    async modal(){

      // console.log(await ListService.GetList(54, this.$router));
      // console.log(await ListService.GetAllLists());

      // console.log(await UserService.Log());

      // console.log(await UserService.refreshToken());

      this.isModal=!this.isModal;
    },
    createList(list){
      this.$emit("create", list)
    }
  },

}
</script>

<style scoped>

.modalA-enter-active, .modalA-leave-active, .modalA-move {
  transition: opacity 0.5s;
}
.modalA-enter, .modalA-leave-to{
  opacity: 0;
}

/*.modal-enter-to, .modal-leave{*/
/*  opacity: 1;*/
/*}*/

.fog{
  position: absolute;
  z-index: 9;
  width: 100vw;
  height: calc(100vh - 60px);
  top:0;
  left:0;

  background: rgba(217, 217, 217, 0.01);
  backdrop-filter: blur(12px);
}

.modal{
  position: absolute;
  z-index: 10;
  top: calc(50% - 60px - 250px);
  left: calc(50% - 175px);
}

</style>