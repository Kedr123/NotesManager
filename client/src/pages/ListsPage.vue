<template>
  <div class="body">
    <Header/>
    <CreateListModal class="button-m" @create="createList"/>
    <ListLists :lists="lists" @delete="deleteList" @save="save"/>
  </div>
</template>

<script>
import Header from "@/components/Semantics/Header";
import CreateListModal from "@/components/ModalWindows/CreateListModal";
import ListLists from "@/components/Lists/ListLists/ListLists";
import ListService from "@/services/ListService";

export default {
  components: {
    ListLists,
    Header, CreateListModal
  },
  async mounted() {
    let response = await ListService.GetAllLists();
    this.lists = response.lists.reverse();
  },
  data() {
    return {
      lists: [
        {
          id: 14,
          title: "Grep",
          file: {id: 80, fileName: "1820c7d8-e51a-4544-ab9a-1f7ce91785de.jpg", oldFileName: "Ответы.docx"},
          user: null
        },
      ],
    }
  },
  methods: {
    deleteList(id) {
      for (let i = 0; i < this.lists.length; i++) {
        console.log(i)
        if (this.lists[i].id == id) {
          this.lists.splice(i, 1);
          console.log(this.lists)
          return;
        }
      }

    },
    save(list) {
      console.log(list)
      for (let i = 0; i < this.lists.length; i++) {
        console.log(i)
        if (this.lists[i].id == list.id) {
          if(list.file==null)
            this.lists[i].title = list.title;
          else
            this.lists[i] = list;
          console.log(this.lists)
          return;
        }
      }
    },
    createList(list) {
      this.lists.unshift(list);
    }
  }
}
</script>

<style scoped>
.body {
  margin: 0;
  padding: 0;
  background: linear-gradient(107.56deg, #45535A 41.15%, #445542 100%);
  min-height: 100vh;
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.button-m {
  margin: 100px;
}
</style>