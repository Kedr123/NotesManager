<template>
  <div class="list-up">
    <div class="list-header">
      <div class="list-save" @click="createList">
        <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
          <rect x="5.53174" y="7.51364" width="11.8333" height="2.14619" rx="1.07309" transform="rotate(69.8543 5.53174 7.51364)" fill="#D2D3D3"/>
          <rect x="17.0972" y="5.32756" width="16.5359" height="1.99112" rx="0.995561" transform="rotate(121.015 17.0972 5.32756)" fill="#D2D3D3"/>
        </svg>

      </div>
      <!--                <div class="list-title">Заметки №2</div>-->
      <div class="list-input">
        <TextInputInnerBoxShadow v-model="title"/>
      </div>
      <div class="list-not-save" @click="$emit('close')">
        <svg width="29" height="29" viewBox="0 0 29 29" fill="none" xmlns="http://www.w3.org/2000/svg">
          <rect x="7.71436" y="6.17994" width="20.0509" height="2.00509" rx="1.00254" transform="rotate(45 7.71436 6.17994)" fill="#D2D3D3"/>
          <rect x="21.75" y="7.85564" width="19.6498" height="2.00508" rx="1.00254" transform="rotate(135 21.75 7.85564)" fill="#D2D3D3"/>
        </svg>
      </div>
    </div>
    <div class="img">
      <label class="list-img-label">
        <svg width="100" height="77" viewBox="0 0 100 77" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path fill-rule="evenodd" clip-rule="evenodd" d="M27.4539 52.8652C27.4539 50.1038 25.2153 47.8652 22.4539 47.8652H5C2.23858 47.8652 0 50.1038 0 52.8652V72C0 74.7614 2.23858 77 5 77H94.7305C97.4919 77 99.7305 74.7614 99.7305 72V52.8652C99.7305 50.1038 97.4919 47.8652 94.7305 47.8652H78.9575C76.196 47.8652 73.9575 50.1038 73.9575 52.8652V57.4326C73.9575 60.1941 71.7189 62.4326 68.9575 62.4326H32.4539C29.6925 62.4326 27.4539 60.194 27.4539 57.4326V52.8652Z" fill="white"/>
          <rect x="44" width="14.0071" height="31.3759" rx="3" fill="white"/>
          <path d="M52.9009 46.6644C52.1043 47.7955 50.4274 47.7955 49.6307 46.6644L38.6866 31.1269C37.7533 29.8019 38.701 27.9752 40.3217 27.9752L62.21 27.9752C63.8307 27.9752 64.7784 29.8019 63.8451 31.1269L52.9009 46.6644Z" fill="white"/>
        </svg>
        <input type="file" @change="uploadImage" class="list-download-img">
      </label>
      <img :src="preview" alt="">
    </div>
  </div>
</template>

<script>
import TextInputInnerBoxShadow from "@/components/UI/TextInputs/TextInputInnerBoxShadow";
import ListValidator from "@/validators/ListValidator";
import ListService from "@/services/ListService";
import  defaultImg from "@/assets/defaulte.png";
export default {
  components: {TextInputInnerBoxShadow},
  data(){
    return{
      preview:defaultImg,
      file:null,
      title:"",
    }
  },
  methods:{
    uploadImage(e){
      this.file = e.target.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(this.file);
      reader.onload = e =>{
        this.preview = e.target.result;
        // console.log(this.preview);
      };
    },
    async createList(){
      let valid = ListValidator.CreateListValidator(this.title, this.file);

      if(valid){
        return;
      }

      let response = await ListService.CreateList(this.title,this.file);

      console.log(response);

      if(!(response?.result==true)){
        return;
      }

      this.$emit('create', response.response.list);
      this.title="";
      this.file=null;
    }

  }
}
</script>

<style scoped>

.list-header{
  background: #484C4D;
  border-radius: 10px;
  filter: drop-shadow(0px 4px 4px rgba(0, 0, 0, 0.25));

  display: flex;
  justify-content: space-between;
  align-items: center;

  width: 100%;
  height: 48px;


}

.img{
  display: flex;
  align-items: end;

  width: 350px;
  height: 197px;

}

.img img{
  width: 100%;
  height: 100%;
  border-radius: 10px;
  /*position: relative;*/

}

/************Состояние изменения листа**************************/

.list-up{
  margin-top: 50px;
  width: 350px;
  height: 250px;
  background-color: #435051;
  border-radius: 10px;
  filter: drop-shadow(0px 4px 4px rgba(0, 0, 0, 0.25));

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  transition-duration: 0.5s;
}

.list-save{
  display: flex;
  align-items: center;
  margin-left: 25px;
  transition-duration: 0.3s;
}

.list-save:hover {
  transform: scale(1.5,1.5);
}

.list-not-save{
  display: flex;
  align-items: center;
  margin-right: 25px;
  transition-duration: 0.3s;
}


.list-not-save:hover {
  transform: scale(1.5,1.5);
}

/*********************Инпут************************/

.list-input{
  width: 65%;
  height: 35px;

  display: flex;
  justify-content: center;
  align-items: center;

}

.list-input-title{
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

/*************************Стилизация загрузки********************************/

.list-img-label{
  position: absolute;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 197px;

  background-color: #000000;
  opacity: 0.35;

  border-radius: 10px;

  transition-duration: 0.5s;
}

.list-img-label svg{
  opacity: 00.35;
  transition-duration: 0.5s;
}

.list-img-label:hover{
  opacity: 0.9;
}

.list-img-label:hover svg{
  opacity: 1;
}

.list-download-img{
  display: none;
}

</style>