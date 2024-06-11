<script setup>
import { ref } from 'vue'
import Swal from 'sweetalert2'
import { dataStore } from '../stores/mipinia';
const store = dataStore();
const numbers = ref(Array.from({ length: 37 }, (_, i) => i))
const activeSelect = ref(null);
const selectedColor = ref('');
const parimparApostado = ref('');
const parimparcolorApostado = ref('');
const numerApostado = ref('');
const numerocolorApostado = ref('');

const handleSelectChange = (event,selectNumber) => {
  if (event.target.value === "") {
   
    activeSelect.value = null;
   store.updateIndexes({ 
      index1: 0,
      index2: 0, 
      index3: 0 
    });
    store.updateColNumParApostado({ 
      colorApostado: '',
      numerocolorApostado: '', 
      parimparcolorApostado: '' 
    });
   store.updateBtnApuestaValue(true);
  } else {
      if(selectNumber == 1){
          store.updateColNumParApostado({ 
            colorApostado: event.target.value,
          });
          store.updateIndexes({index1:selectNumber})
      }
      if(selectNumber == 2){
        store.updateColNumParApostado({ 
            parimparcolorApostado: `${parimparApostado.value}-${parimparcolorApostado.value}` ,
          });
          store.updateIndexes({index2:selectNumber})
      }
      if(selectNumber == 3){
        store.updateColNumParApostado({ 
            numerocolorApostado: `${numerApostado.value}-${numerocolorApostado.value}` ,
          });
          store.updateIndexes({index3:selectNumber})
      }
      store.updateBtnApuestaValue(false);
    activeSelect.value = selectNumber;
  }
};

const isDisabled = (selectNumber) => {
  return activeSelect.value !== null && activeSelect.value !== selectNumber;
};

function btnApostar(){
  store.updateBtnApuestaValue(true);
  store.updateBtnGirarValue(false);
  
}



</script>

<template>
    <div class="card mt-4">
      <div class="card-body">
            <form action="">
              <div class="row align-items-center ">
                <h3>Apostar por:</h3>
                <div class="col-md-3">
                  <select class="form-select" id="selectColor" v-model="selectedColor" @change="handleSelectChange($event, 1)" :disabled="isDisabled(1)" >
                    <option selected value="">Escoger Color...</option>
                    <option value="rojo">Rojo</option>
                    <option value="negro">Negro</option>
                  </select>
                </div>
                <div class="col-md-3">
                  <div class="input-group">
                  <select class="form-select" id="selectParimpar" @change="handleSelectChange($event, 2)" :disabled="isDisabled(2)" v-model="parimparApostado" >
                    <option selected value="">Par/Impar...</option>
                    <option value="par">Par</option>
                    <option value="impar">Impar</option>
                  </select>
                  <select class="form-select" id="selectParimparcolor" @change="handleSelectChange($event,2)" :disabled="isDisabled(2)" v-model="parimparcolorApostado" >
                    <option selected value="">Color...</option>
                    <option value="rojo">Rojo</option>
                    <option value="negro">Negro</option>
                  </select>
                </div>
                </div>
                <div class="col-md-3">
                  <div class="input-group">
                    <select class="form-select" id="selectNumero"  @change="handleSelectChange($event,3)" :disabled="isDisabled(3)" v-model="numerApostado" >
                    <option disabled selected value="">Número...</option>
                    <option v-for="number in numbers" :key="number" :value="number">
                        {{ number }}
                    </option>
                  </select>
                    <select class="form-select" id="selectNumerocolor"  @change="handleSelectChange($event,3)" :disabled="isDisabled(3)" v-model="numerocolorApostado" >
                    <option selected value="">Color...</option>
                    <option value="rojo">Rojo</option>
                    <option value="negro">Negro</option>
                  </select>
                  </div>
                </div>
                <div class="col-md-3 text-end">
                    <button class="btn btn-warning btn-lg fs-4 fw-bold" :disabled="store.btnApuestaDisabled" @click.prevent="btnApostar()" >Apostar: ${{ store.apuesta }}</button>
                </div>
              </div>
              
            </form>
      </div>
  </div>
</template>



<style>

</style>