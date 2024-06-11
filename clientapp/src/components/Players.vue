<script setup>
import {ref,onMounted,computed } from 'vue'
import dataservice from "../services/dataservice";

const datos = ref([]);
const busqueda = ref('');
function cargarJugadores(){
    dataservice.returnPlayers()
    .then(response => response.json())
    .then(data => datos.value = data)
         .catch(error => console.error(error));
}

/*const sortedDatos = computed(() => {
  return [...datos.value].sort((a, b) => b.monto - a.monto);
});*/

const sortedDatos = computed(() => {
  const filteredDatos = buscar();
  return filteredDatos.sort((a, b) => b.monto - a.monto);
});

const buscar = () => {
  const filteredDatos = datos.value.filter(dato => dato.nombre.toLowerCase().includes(busqueda.value.toLowerCase()));
  return filteredDatos;
}

onMounted(() => {
    cargarJugadores();
});

</script>


<template>
    <div class="row justify-content-center">
        <div class="col-md-6">
            <h2 class="text-center text-white mb-3">Saldo de Jugadores</h2>
            <input type="text" class="form-control mb-3" placeholder="Buscar por nombre..." v-model="busqueda" @change="buscar">
                <ul class="list-group">
                    <li v-for="item in sortedDatos" :key="item.id" class="list-group-item d-flex justify-content-between align-items-center text-capitalize fw-bold"  >
                        {{ item.nombre }}
                        <span class="badge text-bg-primary rounded-pill"> $ {{ item.monto }}</span>
                    </li>
                </ul>
        </div>
    </div>

</template>


<style>

</style>