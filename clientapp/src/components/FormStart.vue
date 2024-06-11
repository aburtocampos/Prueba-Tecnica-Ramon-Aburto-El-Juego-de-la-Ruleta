<script setup>
import {ref,onMounted} from 'vue'
import { RouterLink } from 'vue-router'
import { useRouter } from 'vue-router'
import {dataStore} from '../stores/mipinia'
import dataservice from "../services/dataservice";
const router = useRouter()
const store = dataStore()
const datos = ref([])
const flag = ref(false)

const formData = ref({
  nombre: '',
  monto: '',
  apuesta: ''
});


function handleNameChange(nombre){
  dataservice.returnPlayerByName(nombre)
      .then(response => {
        if (response.status === 204) {
          console.log('No content');
          return null;
        } else if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        } else {
          return response.json(); 
        }
      })
    .then(function(data) {
      if (data !== null) {
        console.log("me traje",data)
                console.log('data.monto',data.monto);
                formData.value.monto = data.monto;

    }else{

    }
     
    })
    .catch(error => console.error(error));
}

function apuestaMenor(){
  if(formData.value.apuesta > formData.value.monto){
    flag.value = true;
    return true;
  }else{
    flag.value = false;
    return false;
  }
}

function saveData(){
  if(!apuestaMenor()){
    store.updateFormData(formData.value);
router.push("/ruleta");
  }
    
}

onMounted(() => {
 if(store.isNotEmpty()){
    console.log('datos vacios',store);
  }else{
    store.updateFormData({ 
          nombre: '',
          monto: '', 
          apuesta: ''
        });
    store.updateColNumParData({ 
          colorRuleta: '',
          parimpar: '', 
          numero: ''
        });
    store.updateColNumParApostado({ 
      colorApostado: '',
      numerocolorApostado: '', 
      parimparcolorApostado: '' 
    });
    store.updateIndexes({ 
      index1: 0,
      index2: 0, 
      index3: 0 
    });
    store.updateBtnGirarValue(true);
    store.updateBtnApuestaValue(true);
   
  }
});
</script>

<template>
  <form class="text-center pt-2" @submit.prevent="saveData()" >
    <div class="row justify-content-center">
      <h1 class="h3 mb-3 fw-normal text-white">Bienvenido a El Juego de la Ruleta</h1>
      <div class="col-md-6">
        <div class="form-floating mb-3">
      <input type="text" class="form-control fw-bold text-capitalize fs-4" id="nombre" v-model="formData.nombre"  @change="handleNameChange(formData.nombre)" required placeholder="Ingresa nombre...">
      <label for="nombre">Nombre:</label>
    </div>
    <div class="form-floating mb-3">
      <input type="number" class="form-control fw-bold fs-4" id="monto" v-model.number="formData.monto" required placeholder="Ingresa tu Saldo">
      <label for="monto">Saldo:</label>
    </div>
    <div class="form-floating mb-3">
      <input type="number" class="form-control fw-bold fs-4" id="apuesta" v-model.number="formData.apuesta" @change="apuestaMenor()" required placeholder="Ingresa Monto a Apostar...">
      <label for="apuesta">Monto a Apostar:</label>
      <div v-if="flag" class="alert alert-danger d-flex align-items-center rounded-0 fw-bold mt-2" role="alert">
      <div>
        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-alert-triangle"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 9v4" /><path d="M10.363 3.591l-8.106 13.534a1.914 1.914 0 0 0 1.636 2.871h16.214a1.914 1.914 0 0 0 1.636 -2.87l-8.106 -13.536a1.914 1.914 0 0 0 -3.274 0z" /><path d="M12 16h.01" /></svg>
        El monto de la apuesta es mayor que el monto actual de su saldo.
      </div>
    </div>
    </div>
      <button class="btn btn-primary w-100 py-3 fs-4 fw-bold" >Iniciar Juego</button>
      </div>
    </div>
  </form>
</template>
