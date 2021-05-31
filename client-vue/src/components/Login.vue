<template>
  <div className="login-container">
    <section className="form">
        <img :src="logo_img" alt="REST API Logo" />

        <form @submit.prevent="() => console.log('vai submmitar')">
            <h1>Access your account</h1>
            
            <input placeholder="Username" 
                :value="username"
                @change="setUsername" />

            <input type="password" 
                placeholder="Password" 
                :value="password" 
                @change="setPassword" />

            <button className="button" type="submit">Login</button>
        </form>
    </section>

    <img :src="padlock_img" alt="Login" />
</div>
</template>

<script>
export default {

}
</script>

<style>

</style>