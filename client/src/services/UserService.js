import axios from "axios";

export default class UserService {

    // static url = "http://localhost:5021/api/";
    static url = "https://localhost:7279/api/";
    // static url = "https://localhost:44312/api/";

    static async registration(email, password) {

        // console.log(this.url)
        let response;
        let formData = new FormData();
        formData.append("email",email);
        formData.append("password",password);

        await axios.post(this.url + "user", formData).then(resp => {
            response = resp.data
        }).catch(error => {
            console.log(error)
            if(error?.response) response = error.response.data;
            else response = error;
        });
        console.log(response)
        return response;
    }
    static async authorisation(email, password) {

        // console.log(this.url)
        let response;
        // let formData = new FormData();
        // formData.append("email",email);
        // formData.append("password",password);

        await axios.post(this.url + "auth?email="+email+"&password="+password).then(resp => {
            response = resp.data
        }).catch(error => {
            console.log(error)
            if(error?.response) response = error.response.data;
            else response = error;
        });
        console.log(response)
        return response;
    }

}