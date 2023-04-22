import axios from "axios";
import router from "@/router/router";

export default class UserService {

    // static url = "http://localhost:5021/api/";
    static url = "https://localhost:7279/api/";
    // static url = "https://192.168.3.179:7279/api/";

    // static url = "https://localhost:44312/api/";

    static async registration(email, password) {

        // console.log(this.url)
        let response;
        let formData = new FormData();
        formData.append("email", email);
        formData.append("password", password);

        await axios.post(this.url + "user", formData).then(resp => {
            response = resp.data
        }).catch(error => {
            console.log(error)
            if (error?.response) response = error.response.data;
            else response = error;
        });
        console.log(response)
        return response;
    }

    static async authorisation(email, password) {
        let response;


        await axios.post(this.url + "auth?email=" + email + "&password=" + password,{},{
            headers: {
                "Content-Type": "application/json"
            },
            withCredentials: true,
        }).then(resp => {
            response = {
                response:resp.data,
                result:true
            }
        }).catch(error => {
            console.log(error)
            if (error?.response) response = {
                response:error.response.data,
                result:false
            };
            else response = {
                response:error,
                result:false
            };
        });
        console.log(response)
        return response;
    }

    static async refreshToken() {
        let response = false;
        await axios.post(this.url + "auth/refresh",{},{
            withCredentials:true
        })
            .then(res => {
                localStorage.setItem("token", res.data?.jwtAccess);
                response = true;
            }).catch(() => {
                localStorage.removeItem("token");
                router.push("/");
            });
        return response;
    }

    static async Log() {
        let response = false;
        await axios.post(this.url + "auth/log")
            .then(res => {
                console.log(res)
                response = true;
            }).catch((error) => {
                console.log(error)
                // localStorage.removeItem("token");
                // this.$router({name:'auth'})
            });
        return response;
    }

    static async Logout(){
        let respons=false;

        await axios.get(this.url+"auth").then(()=>{
            respons=true;
        }).catch(()=>{
            respons =false;
        })

        return respons;
    }

}