<template>
  <div>
    <ListListsItemBase :list="list" v-if="!isUp" @delete="deleteList" @edit="isUp=!isUp" />
    <ListListsItemUpdate :list="list" v-else @save="save" @close="isUp=!isUp" />
  </div>
</template>

<script>
import ListListsItemBase from "@/components/Lists/ListLists/ListListsItemBase";
import ListListsItemUpdate from "@/components/Lists/ListLists/ListListsItemUpdate";
export default {
  components: {ListListsItemUpdate, ListListsItemBase},
  props:{
    list:{
      type:Object,
      required:true
    }
  },
  data(){
    return{
      isUp:false
    }
  },
  methods:{
    deleteList(){
      let id = this.list.id
      this.$emit('deleteL', id)
    },
    save(list){
      this.isUp=!this.isUp;
      this.$emit('save',list);
    }
  }
}
</script>

<style scoped>

</style>