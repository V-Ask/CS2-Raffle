<script>
import axios from 'axios';
import { ref } from 'vue';
import Loading from 'vue-loading-overlay';

export default {
    data() {
        return {
            disabled: false,
            password: ref(""),
            LOGIN_TIMEOUT: 2,
            fullPage: true
        }
    },
    methods: {
        async login() {
            this.disabled = true;
            const path = 'http://localhost:5000/login';
            axios.post(path, {
                    password: this.password
                }).then((res) => {
                    localStorage.setItem('access_token', res.data['token']);
                    window.location.href = '/'
                })
                .catch((error) => {
                    console.error(error);
                });
            setTimeout(() => {
                this.disabled = false;
            }, this.LOGIN_TIMEOUT * 1000);
        }
    }, 
    mounted() {
        let token = localStorage.getItem('access_token');
        if (token === null) {
            return;
        }
        this.disabled = true;
        const path = 'http://localhost:5000/auth';
        axios.get(path, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
        .then(() => window.location.href = '/')
        .catch((error) => {
            this.disabled = false;
            console.log(error)
        });
    },
    components: {
        Loading
    }
}
</script>
<style>
.password-input {
    width: 50%;
    display: flex;
    flex-direction: column;
    gap: 10px;
}

</style>
<template>
<loading v-model:active="disabled" :is-full-page="fullPage"/>
<div class="background">
    <div class="blur">
        <div class="password-input">
            <span>Login to Website:</span>
            <input type="password" v-model="password" placeholder="Insert Server Password..." @keydown.enter="login" />
            <button v-if="!disabled" @click="login">Login...</button>
        </div>
    </div>
</div>
</template>