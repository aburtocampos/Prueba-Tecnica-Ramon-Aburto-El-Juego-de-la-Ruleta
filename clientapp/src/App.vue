<script setup>
import { ref, onMounted } from 'vue';
import { RouterLink, RouterView } from 'vue-router'
import {dataStore} from './stores/mipinia'
const store = dataStore();

onMounted(() => {
 
});
</script>

<template>
  <div class="container">
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
  <div class="container ">
    <RouterLink class="navbar-brand" to="/">Ruleta (.Net Api + Vuejs3)</RouterLink>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul v-if="!store.isNotEmpty()" class="navbar-nav ms-auto mb-2 mb-lg-0">
        <li class="nav-item me-2">
          <RouterLink class="btn btn-outline-primary fw-bold" to="/ruleta">Ruleta</RouterLink>
        </li>
        <li class="nav-item me-2">
          <RouterLink class="btn btn-outline-dark fw-bold" to="/players">Jugadores</RouterLink>
        </li>
        <li class="nav-item">
          <RouterLink class="active btn btn-outline-danger fw-bold" aria-current="page" to="/">Salir</RouterLink>
        </li>
       
      </ul>
    </div>
  </div>
</nav>

  <RouterView class="pt-4" />
  </div>

</template>

<style scoped>


</style>
