import axios from "axios";
import UserService from "@/services/UserService";


export default class ColumnService {

    // static url = "http://localhost:5021/api/";
    static url = "https://localhost:7279/api/";
    // static url = "https://192.168.3.179:7279/api/";

    // static url = "https://localhost:44312/api/";

    static async GetColumn(ColumnId) {

        let response;

        await axios.get(this.url + "column/" + ColumnId, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(resp => {
            response = {
                response: resp.data,
                result: true
            }
        }).catch(async error => {
            if (error?.response?.status == 401 || error?.status == 401) {
                if (await UserService.refreshToken()) {
                    response = await this.GetColumn(ColumnId);
                }
            } else {
                response = {error: error.message};
            }

        });
        return response;
    }


    static async GetAllColumnsList(ListId) {
        let response;

        await axios.get(this.url + "column/list/" + ListId, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res => {
            response = {
                response: res.data,
                result: true
            }
        }).catch(async error => {
            if (error?.response?.status == 401 || error?.status == 401) {
                if (await UserService.refreshToken()) {
                    response = await this.GetAllColumnsList(ListId);
                }
            } else {
                response = {error: error.message};
            }
        });

        return response;
    }

    static async CreateColumn(title, ListId) {
        let formData = new FormData();
        let response;
        formData.append("title", title);
        formData.append("listid", ListId);

        await axios.post(this.url + "column", formData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res => {
            response = {
                response: res.data,
                result: true
            };
        }).catch(async error => {
            if (error?.response?.status == 401 || error?.status == 401) {

                if (await UserService.refreshToken()) {
                    response = await this.CreateColumn(title, ListId);
                }

            } else {
                response = {error: error.message}
            }
        });

        return response
    }

    static async UpdateColumn(ColumnId, title, ListId) {
        let formData = new FormData();
        let response;
        formData.append("title", title);
        formData.append("id", ColumnId);
        formData.append("listid", ListId);

        await axios.put(this.url + "column", formData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res => {
            response = {
                response: res.data,
                result: true
            };
        }).catch(async error => {
            if (error?.response?.status == 401 || error?.status == 401) {

                if (await UserService.refreshToken()) {
                    response = await this.UpdateColumn(ColumnId, title, ListId);
                }

            }  else {
                response = {error: error.message}
            }
        });

        return response
    }

    static async DeleteColumn(ColumnId) {
        let response;

        await axios.delete(this.url + "column/" + ColumnId, {
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
                    response = await this.DeleteColumn(ColumnId);
                }

            }  else {
                response = {error: error.message}
            }
        });

        return response
    }
}