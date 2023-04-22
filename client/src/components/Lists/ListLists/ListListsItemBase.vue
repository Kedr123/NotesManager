<template>

  <div class="list">
    <div class="list-header">
      <div class="list-update" @click="$emit('edit')">
        <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path fill-rule="evenodd" clip-rule="evenodd" d="M18.3332 16.6667V18.3333H1.6665V16.6667H18.3332ZM12.4998 1.66666L16.6665 5.83333L7.49984 15H3.33317V10.8333L12.4998 1.66666ZM9.75555 6.76756L4.99984 11.5233V13.3333H6.80984L11.5656 8.57756L9.75555 6.76756ZM12.4998 4.02333L10.9341 5.58905L12.7441 7.39905L14.3098 5.83333L12.4998 4.02333Z" fill="white" fill-opacity="0.75"/>
        </svg>

      </div>
      <div class="list-title">{{list.title}}</div>
      <div class="list-delete" @click="deleteList" >
        <svg width="17" height="20" viewBox="0 0 17 20" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M1.1671 5.10434C1.01619 4.47551 1.49281 3.87096 2.13949 3.87096H15.7803C16.4114 3.87096 16.8847 4.44829 16.7609 5.06708L13.9351 19.1961C13.8416 19.6635 13.4312 20 12.9545 20H5.53046C5.06807 20 4.66598 19.683 4.55807 19.2334L1.1671 5.10434Z" fill="#A95A5A"/>
          <path d="M3.67416 6.66847C3.55855 6.05297 4.03071 5.48387 4.65697 5.48387H13.8593C14.4855 5.48387 14.9577 6.05297 14.8421 6.66847L12.8951 17.0341C12.8063 17.5069 12.3934 17.8495 11.9123 17.8495H6.6039C6.1228 17.8495 5.7099 17.5069 5.62109 17.0341L3.67416 6.66847Z" fill="#4B4F51"/>
          <rect width="1.28696" height="13.9313" transform="matrix(0.994036 -0.109056 0.156048 0.98775 4.7417 4.61916)" fill="#A95A5A"/>
          <rect x="8.61279" y="4.40861" width="1.29032" height="13.8794" fill="#A95A5A"/>
          <rect width="1.28696" height="13.9313" transform="matrix(0.994036 0.109056 -0.156048 0.98775 12.7222 4.40862)" fill="#A95A5A"/>
          <rect x="0.871094" y="1.93549" width="16.129" height="1.29032" rx="0.645161" fill="#A95A5A"/>
          <rect x="7.96777" width="1.93548" height="2.58065" rx="0.967742" fill="#A95A5A"/>
        </svg>
      </div>
    </div>
    <div class="img">
      <img @error="isError"  :src="img" alt="">
<!--      <img :src="'https://192.168.3.179:7279/Files/'+list?.file?.fileName" alt="">-->
    </div>
  </div>
</template>

<script>
import defaultImg from "@/assets/defaulte.png"
import ListService from "@/services/ListService";
export default {

  data(){
    return{
      preview:null,
      img:'https://localhost:7279/Files/'+this.list?.file?.fileName
    }
  },
  props:{
    list:{
      type:Object,
      required:true
    }
  },
  methods:{
    uploadImage(e){
      const image = e.target.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(image);
      reader.onload = e =>{
        this.preview = e.target.result;
        // console.log(this.preview);
      };
    },
    isError(){
      this.img = defaultImg;
    },
    async deleteList(){
      let response = await ListService.DeleteList(this.list.id)
      console.log(response)
      if(response?.result==true) this.$emit('delete')
    }
  }
}
</script>

<style scoped>

.list, .list-up{
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

.list:hover{
  box-shadow: 0px 8px 8px rgba(0, 0, 0, 0.25);
}

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

.list-title{
  font-family: 'Roboto Light';
  font-style: normal;
  font-weight: 300;
  font-size: 24px;
  line-height: 28px;

  color: #D9D9D9;

}

.list-update{
  display: flex;
  align-items: center;

  margin-left: 25px;

  transition-duration: 0.3s;
}

.list-update:hover {
  transform: scale(1.5,1.5);
}

.list-delete{
  display: flex;
  align-items: center;

  margin-right: 25px;

  transition-duration: 0.3s;
}


.list-delete:hover {
  transform: scale(1.5,1.5);
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


.lists{
  display: flex;
  justify-content: space-around;
  flex-wrap: wrap;
  max-width: 1300px;
  margin-bottom: 300px;
  /*max-height: 700px;*/
  /*overflow: auto;*/
  /*overflow-scrolling: auto;*/
}

</style>