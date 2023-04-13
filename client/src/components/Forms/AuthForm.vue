<template>
  <div class="auth-form">
    <div class="title">
      Заметки
    </div>

    <!--    <div class="confirmation">-->
    <!--      <div class="confirmation-block">-->
    <!--        <svg width="25" height="25" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">-->
    <!--          <circle cx="12.5" cy="12.5" r="12" stroke="white"/>-->
    <!--          <rect width="10.1755" height="1.84551" rx="0.922753" transform="matrix(0.348329 0.928011 -0.949505 0.340444 8.81104 9.3866)" fill="white"/>-->
    <!--          <rect width="14.2192" height="1.71217" rx="0.856083" transform="matrix(-0.521135 0.847161 -0.866783 -0.509337 18.8693 7.52844)" fill="white"/>-->
    <!--        </svg>-->

    <!--        <div class="confirmation-block-text">-->
    <!--          Регистрация-->
    <!--        </div>-->
    <!--      </div>-->
    <!--    </div>-->

    <div class="inputs">
      <div v-if="registrationConfirmation" class="confirmation">
        <div class="confirmation-block">
          <svg width="25" height="25" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
            <circle cx="12.5" cy="12.5" r="12" stroke="white"/>
            <rect width="10.1755" height="1.84551" rx="0.922753"
                  transform="matrix(0.348329 0.928011 -0.949505 0.340444 8.81104 9.3866)" fill="white"/>
            <rect width="14.2192" height="1.71217" rx="0.856083"
                  transform="matrix(-0.521135 0.847161 -0.866783 -0.509337 18.8693 7.52844)" fill="white"/>
          </svg>

          <div class="confirmation-block-text">
            Регистрация
          </div>
        </div>
      </div>
      <TextInputBorderWhite class="indent" v-model="email" :placeholder="'E-mail'" :type="'email'"/>
      <div :class="errors.email?'error':'no-error'"> {{ errors.email }}</div>


      <TextInputBorderWhite class="indent" v-model="password" :placeholder="'Password'" :type="'password'"/>
      <div :class="errors.password?'error':'no-error'"> {{ errors.password }}</div>
      <TextInputBorderWhite v-if="isReg" class="confirm-password" v-model="confirmPassword"
                            :placeholder="'Confirm Password'" :type="'password'"/>
      <div v-if="isReg" :class="errors.confirm?'error':'no-error'"> {{ errors.confirm }}</div>


    </div>

    <div class="buttons">
      <ButtonBorderWhite :value="isReg?'Войти':'Регистрация'" @click="isReg=!isReg"/>
      <ButtonBorderWhite :value="'Отправить'" @click="auth"/>
    </div>

  </div>
</template>

<script>
import TextInputBorderWhite from "@/components/UI/TextInputs/TextInputBorderWhite";
import ButtonBorderWhite from "@/components/UI/Buttons/ButtonBorderWhite";
import UserService from "@/services/UserService";
import UserValidator from "@/validators/UserValidator";

export default {
  components: {
    TextInputBorderWhite,
    ButtonBorderWhite
  },
  name: 'AuthForm',
  data() {
    return {
      errors: {
        email: "",
        password: "",
        confirm: "",
      },
      email: "",
      password: "",
      confirmPassword: "",
      isReg: false,
      registrationConfirmation:false,
    }
  },
  methods: {
    async auth() {

      if (this.isReg) {

        let valid = UserValidator.RegistrationValidator(this.email, this.password, this.confirmPassword)

        if (valid) {
          console.log(valid)
          this.errors.email = valid.email;
          this.errors.password = valid.password;
          this.errors.confirm = valid.passwordConfirm;
          return;
        }

        let res = await UserService.registration(this.email, this.password);

        if (res?.errors) {
          this.errors.password = res.errors?.Password ? res.errors?.Password : res.errors?.password;
          this.errors.email = res.errors?.Email ? res.errors?.Email : res.errors?.email;
          return;
        }

        this.errors.email = "";
        this.errors.password = "";
        this.errors.confirm = "";

        this.email = "";
        this.password = "";
        this.confirmPassword = "";

        this.registrationConfirmation = true;
        this.isReg = false;


      } else {
        let valid = UserValidator.AuthorisationValidator(this.email, this.password)

        if (valid) {
          console.log(valid)
          this.errors.email = valid.email;
          this.errors.password = valid.password;
          this.errors.confirm = valid.passwordConfirm;
          return;
        }

        let res = await UserService.authorisation(this.email, this.password);

        if (res?.errors) {
          this.errors.password = res.errors?.Password ? res.errors?.Password : res.errors?.password;
          this.errors.email = res.errors?.Email ? res.errors?.Email : res.errors?.email;
          return;
        }


        localStorage.setItem("token", res?.jwtAccess);
        this.$router.push({name:"ListsPage"});
      }
    }
  }
}
</script>

<style scoped>
.auth-form {
  filter: drop-shadow(0px 4px 6px #000000);
  width: clamp(320px, 100vw, 590px);
  height: clamp(260px, 100vw, 480px);

  background: #494D4D;
  border-radius: 10px;

  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  padding: clamp(20px, 5vw, 80px) clamp(20px, 5vw, 70px);

  margin: 0 35px;
}

.title {
  font-family: 'RobotoThin';
  font-weight: 100;
  font-style: normal;
  font-size: clamp(24px, 8vw, 64px);
  line-height: clamp(48px, 8vw, 74px);

  color: #FFFFFF;
}

.inputs {
  display: flex;
  flex-direction: column;
  width: 100%;

}

.buttons {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.indent {
  margin-top: 35px;
}

.confirm-password {
  margin-top: 15px;
}

.error {
  font-family: 'Roboto';
  font-style: normal;
  font-weight: 300;
  font-size: clamp(10px, 4vw, 20px);
  line-height: 16px;

  color: #A95A5A;

  margin-top: 5px;
  padding: 0px 16px;
}

.no-error {
  opacity: 0;
}


.confirmation {

  display: flex;
  justify-content: center;
  width: 100%;
  /*padding: 0 30%;*/
}

.confirmation-block {

  display: flex;
  align-items: center;
  justify-content: space-between;

  background: rgba(26, 106, 34, 0.33);
  border: 1px solid #FFFFFF;
  border-radius: 20px;

  width: 188px;
  height: 35px;

  padding-left: 9px;
  /*padding-right: 27px;*/
}


.confirmation-block-text {
  font-family: 'RobotoThin';
  font-style: normal;
  font-weight: 100;
  font-size: 20px;
  line-height: 23px;

  color: #FFFFFF;
  margin-right: 27px;
}
</style>