<template>
  <div class="login-container">
    <section class="form">
        <img src="../assets/logo.svg" alt="REST API Logo" />
        
        <form @submit.prevent="doLogin">
            <h1>Access your account</h1>

            <input placeholder="Username" v-model="username" />

            <input type="password" 
                placeholder="Password"
                v-model="password" />

            <button @click="doLogin()">Login</button>
        </form>
    </section>

    <img src="../assets/padlock.png" alt="Login" />
</div>
</template>

<script>
export default {
    name: 'login',
    data() {
        return {
            logo_img: './assets/logo.svg',
            padlock_img: '../assets/padlock.png',
            username: '', 
            password: ''
        }
    },
    methods: {
        doLogin() {
            var data = {
                username: this.username, 
                password: this.password
            };

            console.log('Dados a serem enviados: ', data);

            // envia os dados via axios.post
        }
    }
}
</script>

<style>
h1 {
    color: #8ebd60;
}

.login-container {
    width: 100%;
    max-width: 1120px;
    height: 100vh;
    margin: 0 auto;
    display: flex;
    align-items: center;
    justify-content: space-between;
}

.login-container section.form {
    width: 100%;
    max-width: 350px;
    margin-right: 30px;
}

.login-container section.form img {
    width: 45%;
    margin-left: auto;
    margin-right: auto;
    display: block;
}

.login-container section.form form {
    margin-top: 100px;
}

.login-container section.form form h1 {
    font-size: 32px;
    margin-bottom: 32px;
}
</style>