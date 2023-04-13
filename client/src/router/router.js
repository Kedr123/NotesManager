import AuthPage from "@/pages/AuthPage";
import {createRouter, createWebHistory} from "vue-router";
import ListsPage from "@/pages/ListsPage";

const routes =[
    {
        path:"/",
        name: "auth",
        component: AuthPage
    },
    {
        path:"/main",
        name:"ListsPage",
        component: ListsPage,

    },
];

const router = createRouter({
    routes,
    history:createWebHistory(process.env.BASE_URL)
});

router.beforeEach( (to, from, next)=>{

    if(localStorage.getItem("token") && to.path!="/") next();

    if(localStorage.getItem("token")) next({name:"ListsPage"});


    if(to.path=="/") next();

    next({name:"auth"});
})

export default router