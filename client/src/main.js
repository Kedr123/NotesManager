import { createApp } from 'vue'
import App from './App.vue'
import router from "@/router/router";
import {store} from "core-js/internals/reflect-metadata";

createApp(App)
    .use(router)
    .use(store)
    .mount('#app')
