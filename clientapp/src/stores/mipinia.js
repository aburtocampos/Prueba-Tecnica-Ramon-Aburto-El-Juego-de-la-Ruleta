import { ref, computed, watch } from 'vue'
import { defineStore } from 'pinia'

export const dataStore = defineStore('mipinia', () => {
  const nombre = ref(localStorage.getItem('nombre') || '');
  const monto = ref(localStorage.getItem('monto') || '');
  const apuesta = ref(localStorage.getItem('apuesta') || '');

  const colorRuleta = ref(localStorage.getItem('colorRuleta') || '');
  const parimpar = ref(localStorage.getItem('parimpar') || ''); 
  const numero = ref(localStorage.getItem('numero') || '');

  const index1 = ref(localStorage.getItem('index1') || 0);
  const index2 = ref(localStorage.getItem('index2') || 0);
  const index3 = ref(localStorage.getItem('index3') || 0);

const colorApostado = ref(localStorage.getItem('colorApostado') || '');
const parimparcolorApostado = ref(localStorage.getItem('parimparcolorApostado') || '');
const numerocolorApostado = ref(localStorage.getItem('numerocolorApostado') || '');

  const btnApuestaDisabled = ref(localStorage.getItem('btnApuestaDisabled') || true);
  const btnGirarDisabled = ref(localStorage.getItem('btnGirarDisabled') || true);


  function updateFormData(data) {

    if (data.nombre !== undefined) {
      nombre.value = data.nombre;
      localStorage.setItem('nombre', nombre.value);
    }
    if (data.monto !== undefined) {
      monto.value = data.monto;
      localStorage.setItem('monto', monto.value);
    }
    if (data.apuesta !== undefined) {
      apuesta.value = data.apuesta;
      localStorage.setItem('apuesta', apuesta.value);
    }

  }

  function updateColNumParData(data) {
   console.log("updateColNumParData",data)
   console.log('se le passa al metodo',data)
    if (data.colorRuleta !== undefined) {
      colorRuleta.value = data.colorRuleta;
      localStorage.setItem('colorRuleta', colorRuleta.value);
    }else{
      localStorage.setItem('colorRuleta', '');
    }
    if (data.parimpar !== undefined) {
      parimpar.value = data.parimpar;
      localStorage.setItem('parimpar', parimpar.value);
    }
    if (data.numero !== undefined) {
      numero.value = data.numero;
      localStorage.setItem('numero', numero.value);
    }
  }

  function updateColNumParApostado(data) {
    if (data.colorApostado !== undefined) {
      colorApostado.value = data.colorApostado;
      localStorage.setItem('colorApostado', colorApostado.value);
    }
    if (data.parimparcolorApostado !== undefined) {
      parimparcolorApostado.value = data.parimparcolorApostado;
      localStorage.setItem('parimparcolorApostado', parimparcolorApostado.value);
    }
    if (data.numerocolorApostado !== undefined) {
      numerocolorApostado.value = data.numerocolorApostado;
      localStorage.setItem('numerocolorApostado', numerocolorApostado.value);
    }
  }

  function updateIndexes(data) {
    if (data.index1 !== undefined) {
      index1.value = data.index1;
      localStorage.setItem('index1', index1.value);
    }
    if (data.index2 !== undefined) {
      index2.value = data.index2;
      localStorage.setItem('index2', index2.value);
    }
    if (data.index3 !== undefined) {
      index3.value = data.index3;
      localStorage.setItem('index3', index3.value);
    }
  }

  function updateBtnApuestaValue(data) {
    btnApuestaDisabled.value = data;
    localStorage.setItem('btnApuestaDisabled', btnApuestaDisabled.value);
  }

  function updateBtnGirarValue(data) {
    btnGirarDisabled.value = data;
    localStorage.setItem('btnGirarDisabled', btnGirarDisabled.value);
  }

  const isNotEmpty = () => {
    var res = !(nombre.value && monto.value && apuesta.value);
    return res;
  };

  const isNotRuletaEmpty = () => {
    var res = !(colorRuleta.value && parimpar.value && numero.value);
    return res;
  };
  

  return {nombre,monto,apuesta, updateFormData, isNotEmpty,btnApuestaDisabled,updateBtnApuestaValue,btnGirarDisabled,updateBtnGirarValue,colorRuleta ,parimpar,numero,updateColNumParData,colorApostado,parimparcolorApostado,numerocolorApostado,updateColNumParApostado,isNotRuletaEmpty,updateIndexes, index1,index2,index3 }
})
