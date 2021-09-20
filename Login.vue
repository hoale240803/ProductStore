<template>
  <div class="center-position">
    <div class="q-pa-md" style="max-width: 700px">
      <h1>Login</h1>
      <q-form @submit.prevent.stop="onSubmit" class="q-gutter-md">
        <q-input
          filled
          v-model="form.Username"
          label="Email *"
          hint="Email"
          ref="email"
          :rules="emailRules"
        />

        <q-input
          filled
          type="password"
          v-model="form.Password"
          label="Password *"
          ref="password"
          :rules="passwordRules"
          hint="Password"
        />

        <div class="">
          <q-btn
            label="Login"
            type="submit"
            color="primary"
            style="width: 100%"
            class="q-mt-lg"
          />
        </div>
        <!-- REGISTER & RESET-->
        <div>
          <div class="register_reset row">
            <q-btn
              label="Register"
              to="/register"
              color="primary"
              class="col-5"
            />
            <q-btn
              label="Reset"
              type="reset"
              color="primary"
              flat
              class="q-ml-sm col-5"
            />
          </div>
          <div v-on:click="handleForgotPassword" class="link">
            Forgot password
          </div>
        </div>
      </q-form>

      <div class="social-btn">
        <!-- GOOGLE LOGIN -->
        <p>
          <br />
          <button @click="onGoogleSignIn" class="social-button">
            <img
              alt="Google Logo"
              src="../../assets/social-login/google-logo.png"
            />
          </button>
        </p>

        <!-- FACEBOOK LOGIN -->
        <div>
          <q-btn
            label="FACEBOOK LOGIN"
            v-on:click="facebookLoginHandle"
            color="primary"
          />
        </div>
        <!-- TWITTER LOGIN -->
        <div class="q-ml-lg">
          <q-btn
            label="TWITTER LOGIN"
            v-on:click="twitterLoginHandle()"
            color="primary"
          />
        </div>
      </div>
      <!-- Create an acount -->
      <div>
        <p>
          You don't have an account ? You can
          <router-link to="/register">create one</router-link>
        </p>
      </div>
    </div>

    <div class="q-pa-md" v-show="loading"></div>
  </div>
</template>

<script>
import AuthServices from "../../services/auth/AuthServices";
import { getAuth, signInWithPopup, GoogleAuthProvider } from "firebase/auth";
import firebaseApp from "../../firebase/auth";

export default {
  data() {
    return {
      loading: false,
      isFormError: false,
      form: {
        Username: "",
        Password: "",
      },
      isFromValid: false,
      success: false,
      emailRules: [
        (val) => (val !== null && val !== "") || "Please type your email",
        // (val) => this.isValidEmail(val) || "Please type valid email",
      ],
      passwordRules: [
        (val) => (val !== null && val !== "") || "Please type your password",
        // (val) => this.isStrongPassword(val) || "Please type a strong password",
      ],
      clientId:
        "12256232097-a88mk78c2g5r8u3ilsn0g9t1s26mbnat.apps.googleusercontent.com",
    };
  },
  methods: {
    handleForgotPassword: function () {
      this.$router.push("/forgotPassword");
    },
    googleLoginHandle: function () {},
    facebookLoginHandle: function () {},
    twitterLoginHandle: function () {},
    isEmpty: function (val) {
      if (val !== null || val !== "") {
        return false;
      }
      return true;
    },
    isStrongPassword: function (password) {
      let strongPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$/g;
      let matcher = password.match(strongPassword);
      if (matcher) return true;
      return false;
    },
    isValidEmail: function (email) {
      let isValidEmail =
        /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
      let matcher = email.match(isValidEmail);
      if (matcher) return true;
      return false;
    },
    getEmptyForm: function () {
      this.form = {
        Username: "",
        Password: "",
      };
    },
    onSubmit: function () {
      this.loading = true;
      console.log("myform>>", this.form);
      this.$store.dispatch("auth/login", this.form).then(
        (res) => {
          // alert("login success");
          // this.$router.push("/");

          if (res) {
            this.message = res.message;
            //IF HAS TOKEN => LOGIN SUCESS

            if (res.isAuthenticated) {
              localStorage.setItem("user", JSON.stringify(res));
              this.$q.notify({
                color: "green-4",
                textColor: "white",
                icon: "cloud_done",
                message: "Login Success !",
                position: "top",
              });

              this.$router.push("/");
            }
          }
        },
        (error) => {
          this.loading = false;
          this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString();
        }
      );

      this.loading = false;
    },
    onReset: function () {
      // this.getEmptyForm();
    },
    async onGoogleSignIn() {
      this.$store.dispatch("auth/googleLogin").then(
        (res) => {
          // alert("login success");
          // this.$router.push("/");
          if (res) {
            this.message = res.message;

            //IF HAS TOKEN => LOGIN SUCESS
            if (res.accessToken) {
              localStorage.setItem(
                "user",
                JSON.stringify({ user: res, isLoggedIn: true })
              );
              debugger;
              this.$q.notify({
                color: "green-4",
                textColor: "white",
                icon: "cloud_done",
                message: "Login Success !",
                position: "top",
              });
              this.$router.push("/");
            }
          }
        },
        (error) => {
          this.loading = false;
          this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString();
          this.$q.notify({
            color: "negative",
            textColor: "white",
            icon: "cloud_done",
            message: this.message,
            position: "top",
          });
        }
      );
    },
  },
  mounted() {},
};
</script>

<style scoped>
.register_reset {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.social-btn {
  display: flex;
  align-items: center;
  justify-content: center;
  margin-right: 1rem;
}
.center-position {
  text-align: center;
  display: flex;
  align-items: center;
  justify-content: center;
  min-width: 700px;
}
.social-button {
  width: 75px;
  background: white;
  padding: 10px;
  border-radius: 100%;
  box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2);
  outline: 0;
  border: 0;
}
.social-button:active {
  box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.1);
}
.social-button img {
  width: 100%;
}
.link {
  cursor: pointer;
  color: blue;
  padding: 1rem;
}
</style>
