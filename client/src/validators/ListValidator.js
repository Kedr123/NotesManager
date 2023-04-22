export default class ListValidator {

    static CreateListValidator(title, file){

        let response={
            title:[],
            file:[]
        }

        if(!title){
            response.title.push("Поле обязательно для заполнения");
        }

        if(!file){
            // response.password.push("Поле обязательно для заполнения");
        }

        if(response.title.length==0 && response.file.length==0) return false;

        return response

    }

    static UpdateListValidator(title, file){

        let response={
            title:[],
            file:[]
        }

        if(!title){
            response.title.push("Поле обязательно для заполнения");
        }

        if(!file){
            // response.password.push("Поле обязательно для заполнения");
        }

        if(response.title.length==0 && response.file.length==0) return false;

        return response

    }

}