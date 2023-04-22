<template>
  <div class="lists">
  <ListListsItem  v-for="list in lists" :list="list" :key="list.id" @deleteL="deleteList" @save="save"/>
  </div>
</template>

<script>
import ListListsItem from "@/components/Lists/ListLists/ListListsItem";
export default {
  components: {ListListsItem},
  props:{
    lists:{
      type:Array,
      required:true
    }
  },
  data(){
    return{

    }
  },

  methods:{
    deleteList(id){
      this.$emit('delete', id)
    },
    save(list) {
      this.$emit('save',list);
    }
  }
  // mounted() {
  //   this.lists
  // }
}
</script>

<style scoped>
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