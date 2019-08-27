<template>
  <div class="keeps">
    <h1>Welcome Home {{user.username}}</h1>
    <button v-if="user.id" @click="logout">logout</button>
    <router-link v-else :to="{name: 'login'}">Login</router-link>

    <div class="card" style="width: 18rem;">
      <img class="card-img-top" src=" " alt="Card image cap">
      <div class="card-body">
        <h5 class="title">{{keep.name}}</h5>
        <p class="card-text">{{keep.description}}
        </p>
        <a href="#" class="btn btn-primary">Go somewhere</a>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "home",
    data() {
      return {
        newKeep: {
          name: "",
          image: "",
          description: ""
        }
      }
    },
    mounted() {
      this.$store.dispatch("getKeeps")
    },
    computed: {
      user() {
        return this.$store.state.user;
      },
      keeps() {
        return this.$store.state.keeps
      }

    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      }
    }
  };
</script>