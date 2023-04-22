import axios from "axios";
import UserService from "@/services/UserService";



export default class ListService {

    // static url = "http://localhost:5021/api/";
    static url = "https://localhost:7279/api/";
    // static url = "https://192.168.3.179:7279/api/";

    // static url = "https://localhost:44312/api/";

    static async GetList(ListId) {

        let response;

        await axios.get(this.url + "list/" + ListId, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(resp => {
            console.log(resp)
            response = resp.data
        }).catch(async error => {
            if (error.status == 401) {
                if (await UserService.refreshToken()) {
                    response = await this.GetList(ListId);
                } else response = {
                    list: null,
                    errorAuth: true
                }
            } else {
                response = {list: null};
            }

        });
        console.log(response)
        return response;
    }


    static async GetAllLists() {
        let response;

        await axios.get(this.url + "list", {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res => {
            console.log(res)
            response = res.data
        }).catch(async error => {
            if (error?.response?.status == 401 || error?.status == 401) {
                if (await UserService.refreshToken()) {
                    response = await this.GetAllLists();
                } else response = {
                    lists: null,
                    errorAuth: true
                }


            } else {
                response = {lists: null};
            }
        });

        return response;
    }

    static async CreateList(title, file) {
        console.log("ess")
        let formData = new FormData();
        let response;
        formData.append("title", title);
        formData.append("image", file);

       await axios.post(this.url + "list", formData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res => {
            console.log(res);
            response = {
                response: res.data,
                result: true
            };
        }).catch(async error => {
            console.log(error)
            if (error?.response?.status == 401 || error?.status == 401) {

                if (await UserService.refreshToken()) {
                    response = await this.CreateList(title, file);
                }

            } else if (error?.response?.status == 400 || error?.status == 400) {
                response = error.response
            } else {
                response = {error: "server"}
            }
        });

        return response
    }

    static async UpdateList(title, file, ListId) {
        console.log(file)
        let formData = new FormData();
        let response;
        formData.append("title", title);
        formData.append("image", file);
        formData.append("id", ListId);

       await axios.put(this.url + "list", formData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res => {
            console.log(res);
            response = {
                response: res.data,
                result: true
            };
        }).catch(async error => {
            console.log(error)
            if (error?.response?.status == 401 || error?.status == 401) {

                if (await UserService.refreshToken()) {
                    response = await this.CreateList(title, file);
                }

            } else if (error?.response?.status == 400 || error?.status == 400) {
                response = error.response
            } else {
                response = {error: "server"}
            }
        });

        return response
    }

    static async DeleteList(ListId) {
        let response;

       await axios.delete(this.url + "list/"+ListId, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res => {
            console.log(res);
            response = {
                response: res.data,
                result: true
            };
        }).catch(async error => {
            console.log(error)
            if (error?.response?.status == 401 || error?.status == 401) {

                if (await UserService.refreshToken()) {
                    response = await this.DeleteList(ListId);
                }

            } else {
                response = {error: "server"}
            }
        });

        return response
    }




}