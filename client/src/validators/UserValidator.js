export default class UserValidator {

    static RegistrationValidator(email, password, passwordConfirm){

        let response={
            email:[],
            password:[],
            passwordConfirm:[]
        }

        if(!email){
            response.email.push("Поле обязательно для заполнения");
        }

        if(!password){
            response.password.push("Поле обязательно для заполнения");
        }
        else{
            if(password.length<6 && password>40){
                response.password.push("Пороль должен быть от 6 до 40 символов");
            }
        }

        if(!passwordConfirm){
            response.passwordConfirm.push("Поле обязательно для заполнения");
        }
        else {
            if(password!=passwordConfirm) response.passwordConfirm.push("Пороли не совпадают");
        }

        if(response.email.length==0 && response.password.length==0 && response.passwordConfirm.length==0) return false;

        return response

    }
    static AuthorisationValidator(email, password){

        let response={
            email:[],
            password:[]
        }

        if(!email){
            response.email.push("Поле обязательно для заполнения");
        }

        if(!password){
            response.password.push("Поле обязательно для заполнения");
        }
        else{
            if(password.length<6 && password>40){
                response.password.push("Пороль должен быть от 6 до 40 символов");
            }
        }

        if(response.email.length==0 && response.password.length==0) return false;

        return response

    }

}