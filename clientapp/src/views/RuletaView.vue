<script setup>
import {ref, computed} from 'vue'
import Swal from 'sweetalert2'
import {dataStore} from '../stores/mipinia'
import { useRouter } from 'vue-router'
import ApuestaControles from '../components/ApuestaControles.vue'
import dataservice from "../services/dataservice";

const store = dataStore()
const router = useRouter()
const flag1 = ref(true)
const flag2 = ref(false)
const flag3 = ref(false)

const ValoresAsar = ref({
  numero: '',
  color: '',
  parimpar:''
});

const formData = ref({monto: ''});


function btngirar(){
      flag1.value = false;
      flag2.value = true;
     
    setTimeout(function() {
      flag2.value = false;
      flag3.value = true;
      traerDatos();
   
    //  verificarApuestaColor();
    
      console.log("Han pasado 2 segundos.");
    }, 1000);
}

function refrescarSaldo(nombre){
 return dataservice.returnPlayerByName(nombre)
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
        console.log("me traje de nuevo",data)
                console.log('data.monto nuevo',data.monto);
                formData.value.monto = data.monto;
               store.updateFormData({  monto: formData.value.monto, });
               console.log('store nuevo',store);
               return data.monto;
        }else{
          return null;
        }
     
    })
    .catch(error => console.error(error));
}

function traerDatos(){
  dataservice.returnNumberandcolor()
    .then(response => response.json())
    .then(function(data) {
        console.log("me traje returnNumberandcolor",data)
        store.updateColNumParData({ 
          colorRuleta: data.color,
          parimpar: data.numero % 2 === 0 ? 'par' : 'impar', 
          numero: data.numero 
        });
            if(store.index1){
              verificarApuestaColor();
            }
            if(store.index2){
              verificarApuestaParImparColor(store);
            }
            if(store.index3){
              verificarApuestaNumeroColor();
            }
    })
    .catch(error => {
        console.error('Error fetching data:', error);
     });
}

function verificarApuestaColor(){

  if(store.colorRuleta === store.colorApostado){
      const swalWithBootstrapButtons = Swal.mixin({
          customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-secondary"
          },
          buttonsStyling: false
        });
        swalWithBootstrapButtons.fire({
          title: "Ganaste!",
          text: "Felicidades",
          icon: "success",
          showCancelButton: true,
          confirmButtonText: "Guardar Premio",
          cancelButtonText: "No, cancelar!",
          reverseButtons: true,
          html:`
                <span>Premio:</span>
                <h2 class="fw-bold text-success ">$ ${store.apuesta / 2}</h2>         
              `
        }).then((result) => {
          if (result.isConfirmed) {
            
            dataservice.savePlayer({//guarda monto ganado
              nombre: store.nombre,
              monto: store.monto,
              apuesta: store.apuesta / 2, //ganara la mitad de la apuesta
              }).then(() => {
             refrescarSaldo(store.nombre)
              .then(nuevoMonto => {
                  console.log('lo que regreso si gano', nuevoMonto)
                  swalWithBootstrapButtons.fire({
                    title: "Dato Guardado!",
                    html: `<span>Nuevo Saldo:</span><h3 class="fs-3 fw-bold text-success"> $ ${nuevoMonto}</h3>`,
                    icon: "success"
                  }).then((result) => {
                    if (result.isConfirmed) {
                      router.push('/')
                    }
                  });
                });//refrescando datos

              }, err => err.response.data.errors);

           
          } else if (
            result.dismiss === Swal.DismissReason.cancel
          ) {
            swalWithBootstrapButtons.fire({
              title: "Cancelado",
              text: "Your imaginary file is safe :)",
              icon: "info"
            });
          }
        });
  }else{
    dataservice.savePlayer({ //guarda monto ganado y resta el monto al saldo
              nombre: store.nombre,
              monto: store.monto,
              apuesta: store.apuesta * -1, // si pierde se le resta la apuesta a su saldo
              }).then(() => {
                refrescarSaldo(store.nombre)
                  .then(nuevoMonto => {
                      console.log('lo que regreso si gano', nuevoMonto)
                      Swal.fire({
                      icon: "error",
                      title: "Perdiste!",
                      text: 'Suerte la proxima vez',
                        html:`
                            <ul class="list-group">
                               <li class="list-group-item d-flex justify-content-between align-items-center">
                                Saldo Anterior:
                                <span class="badge text-bg-warning ">$ ${store.apuesta + nuevoMonto}</span>
                              </li>
                              <li class="list-group-item d-flex justify-content-between align-items-center">
                                Apostaste:
                                <span class="badge text-bg-danger ">$ ${store.apuesta}</span>
                              </li>
                              <li class="list-group-item d-flex justify-content-between align-items-center">
                                Nuevo Saldo:
                                <span class="badge text-bg-success ">$ ${nuevoMonto}</span>
                              </li>
                            </ul>      
                      `,
                      }).then((result) => {
                        if (result.isConfirmed) {
                          router.push('/')
                        }
                      });
                    });//refrescando datos

                 

              }, err => err.response.data.errors);
             
                
  }
  store.updateBtnApuestaValue(true);
  store.updateBtnGirarValue(true);
}

function verificarApuestaParImparColor(store){
  var valorparimparcolor = `${store.parimpar}-${store.colorRuleta}`;
  console.log('valorparimparcolor',valorparimparcolor);
  console.log('store.parimparcolorApostado',store.parimparcolorApostado);
  if(valorparimparcolor == store.parimparcolorApostado){
    console.log('ganaste');
    const swalWithBootstrapButtons = Swal.mixin({
          customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-secondary"
          },
          buttonsStyling: false
        });
        swalWithBootstrapButtons.fire({
          title: "Ganaste!",
          text: "Felicidades",
          icon: "success",
          showCancelButton: true,
          confirmButtonText: "Guardar Premio",
          cancelButtonText: "No, cancelar!",
          reverseButtons: true,
          html:`
                <span>Premio:</span>
                <h2 class="fw-bold text-success ">$ ${store.apuesta}</h2>         
              `
        }).then((result) => {
          if (result.isConfirmed) {
            
            dataservice.savePlayer({//guarda monto ganado
              nombre: store.nombre,
              monto: store.monto,
              apuesta: store.apuesta,// gana el total de la apuesta y se suma a su saldo
              }).then(() => {
             refrescarSaldo(store.nombre)//refrescando datos
              .then(nuevoMonto => {
                  swalWithBootstrapButtons.fire({
                    title: "Dato Guardado!",
                    text: `Nuevo Saldo: $ ${nuevoMonto}`,
                    icon: "success"
                  }).then((result) => {
                    if (result.isConfirmed) {
                      router.push('/')
                    }
                  });
                });

              }, err => err.response.data.errors);

           
          } else if (
            result.dismiss === Swal.DismissReason.cancel
            //no se guardara lo ganado
          ) {
            
          }
        });
  }else{
    console.log('perdiste');
    dataservice.savePlayer({ //guarda monto ganado y resta el monto al saldo
              nombre: store.nombre,
              monto: store.monto,
              apuesta: store.apuesta * -1, // si pierde se le resta la apuesta a su saldo
              }).then(() => {
                refrescarSaldo(store.nombre)
                  .then(nuevoMonto => {
                      Swal.fire({
                      icon: "error",
                      title: "Perdiste!",
                      text: 'Suerte la proxima vez',
                        html:`
                            <ul class="list-group">
                               <li class="list-group-item d-flex justify-content-between align-items-center">
                                Saldo Anterior:
                                <span class="badge text-bg-warning ">$ ${store.apuesta + nuevoMonto}</span>
                              </li>
                              <li class="list-group-item d-flex justify-content-between align-items-center">
                                Apostaste:
                                <span class="badge text-bg-danger ">$ ${store.apuesta}</span>
                              </li>
                              <li class="list-group-item d-flex justify-content-between align-items-center">
                                Nuevo Saldo:
                                <span class="badge text-bg-success ">$ ${nuevoMonto}</span>
                              </li>
                            </ul>      
                      `,
                      }).then((result) => {
                        if (result.isConfirmed) {
                          router.push('/')
                        }
                      });
                    });//refrescando datos

              }, err => err.response.data.errors);
  }

}

function verificarApuestaNumeroColor(){
  var valornumerocolor = `${store.numero}-${store.colorRuleta}`;
  console.log('valornumerocolor',valornumerocolor);
  console.log('store.numerocolorApostado',store.numerocolorApostado);
  if(valornumerocolor == store.numerocolorApostado){
    console.log('ganaste');
    const swalWithBootstrapButtons = Swal.mixin({
          customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-secondary"
          },
          buttonsStyling: false
        });
        swalWithBootstrapButtons.fire({
          title: "Ganaste!",
          text: "Felicidades",
          icon: "success",
          showCancelButton: true,
          confirmButtonText: "Guardar Premio",
          cancelButtonText: "No, cancelar!",
          reverseButtons: true,
          html:`
                <span>Premio:</span>
                <h2 class="fw-bold text-success ">$ ${store.apuesta}</h2>         
              `
        }).then((result) => {
          if (result.isConfirmed) {
            
            dataservice.savePlayer({//guarda monto ganado
              nombre: store.nombre,
              monto: store.monto,
              apuesta: store.apuesta * 3,// gana el triple de la apuesta y se suma a su saldo
              }).then(() => {
             refrescarSaldo(store.nombre)//refrescando datos
              .then(nuevoMonto => {
                  swalWithBootstrapButtons.fire({
                    title: "Dato Guardado!",
                    text: `Nuevo Saldo: $ ${nuevoMonto}`,
                    icon: "success"
                  }).then((result) => {
                    if (result.isConfirmed) {
                      router.push('/')
                    }
                  });
                });

              }, err => err.response.data.errors);

           
          } else if (
            result.dismiss === Swal.DismissReason.cancel
            //no se guardara lo ganado
          ) {
            
          }
        });
  }else{
    console.log('perdiste');
    dataservice.savePlayer({ //guarda monto ganado y resta el monto al saldo
              nombre: store.nombre,
              monto: store.monto,
              apuesta: store.apuesta * -1, // si pierde se le resta la apuesta a su saldo
              }).then(() => {
                refrescarSaldo(store.nombre)
                  .then(nuevoMonto => {
                      Swal.fire({
                      icon: "error",
                      title: "Perdiste!",
                      text: 'Suerte la proxima vez',
                        html:`
                            <ul class="list-group">
                               <li class="list-group-item d-flex justify-content-between align-items-center">
                                Saldo Anterior:
                                <span class="badge text-bg-warning ">$ ${store.apuesta + nuevoMonto}</span>
                              </li>
                              <li class="list-group-item d-flex justify-content-between align-items-center">
                                Apostaste:
                                <span class="badge text-bg-danger ">$ ${store.apuesta}</span>
                              </li>
                              <li class="list-group-item d-flex justify-content-between align-items-center">
                                Nuevo Saldo:
                                <span class="badge text-bg-success ">$ ${nuevoMonto}</span>
                              </li>
                            </ul>      
                      `,
                      }).then((result) => {
                        if (result.isConfirmed) {
                          router.push('/')
                        }
                      });
                    });//refrescando datos

              }, err => err.response.data.errors);
  }
}

</script>

<template>
  <div class="pt-2">
    <div class="top">
      <h1 class="text-white"  >Gira la ruleta</h1> 
      <div class="box text-end">
        <h2 class="text-white text-capitalize fs-5">Saldo: <span class="text-warning">$ {{ store.monto }} {{ store.colorRuleta }}{{ ValoresAsar.color }}</span></h2>
        <h2 class="text-white text-capitalize">Jugador: <span class="text-warning">{{ store.nombre }}</span></h2>
      </div>
    </div>
   
    <div class="card">
     
      <div class="card-body">
        <div class="d-flex justify-content-between align-items-center">
          <div class="ruletagrafica">
            <div v-if="flag1" class="ruleta ball negro">?</div>
            <div v-if="flag2" class="spinner-container">
              <svg xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline spinner icon-tabler-rotate-2"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M15 4.55a8 8 0 0 0 -6 14.9m0 -4.45v5h-5" /><path d="M18.37 7.16l0 .01" /><path d="M13 19.94l0 .01" /><path d="M16.84 18.37l0 .01" /><path d="M19.37 15.1l0 .01" /><path d="M19.94 11l0 .01" /></svg>
            </div>
            <div v-if="flag3" :class="[store.colorRuleta, 'ruleta', 'ball']">{{ store.numero }}</div>
          </div>
          <div v-if="!store.isNotRuletaEmpty()" class="datos card gap-1 p-1 flex-row">
            <div class="card">
              <div class="card-body p-1 fw-bold">
                <span class="fw-normal" >Número:</span>   {{ store.numero }}
              </div>
            </div>
            <div class="card">
              <div class="card-body p-1  fw-bold">
                <span class="fw-normal" >Color:</span>  <span :class="store.colorRuleta === 'rojo'?'text-danger text-capitalize':'text-dark text-capitalize'" >{{ store.colorRuleta }}</span> 
              </div>
            </div>
            <div class="card">
              <div class="card-body p-1 text-capitalize fw-bold">
               <span class="fw-normal" >Par/Impar:</span>  {{ store.parimpar }}
              </div>
            </div>
          </div>
          <div class="d-grid">
            <span class="text-success fw-bold destellante" v-if="!store.btnGirarDisabled">Click para girar la ruleta</span>
            <button class="btn btn-success btn-lg fs-4 fw-bold" :disabled="store.btnGirarDisabled" @click="btngirar()" >Girar Ruleta</button>
          </div>
         
        </div>
      </div>
  </div>
<ApuestaControles/>



  </div>
</template>

<style>
.ball {
  width: 3em; 
  height: 3em;
  border-radius: 50%; 
  color: #FFFFFF; 
  font-size: 4rem; 
  font-weight: bold; 
  text-align: center; 
 line-height: 2.8em;
}
.rojo{
  background-color: #ff0000;
}
.negro{
  background-color: #000000;
}

.top{
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.spinner-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: auto;
}
.datos{
  align-self: baseline;
}

.spinner {
  width: 10rem;
  height: 10rem;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  0% {
    transform: rotate(360deg);
  }
  100% {
    transform: rotate(0deg);
  }
}

@keyframes destello {
  0%, 100% {
    opacity: 1;
  }
  50% {
    opacity: 0;
  }
}

.destellante {
  animation: destello 1s infinite;
}

.swal2-actions{
    justify-content: space-around;
    width: 100%;
    padding: 0 1rem;
}

.ruleta{
  -webkit-animation: rotate-center 0.4s ease-in-out both;
	        animation: rotate-center 0.4s ease-in-out both;
}
@-webkit-keyframes rotate-center {
  1000% {
    -webkit-transform: rotate(0);
            transform: rotate(0);
  }
  0% {
    -webkit-transform: rotate(360deg);
            transform: rotate(360deg);
  }
}
@keyframes rotate-center {
  1000% {
    -webkit-transform: rotate(0);
            transform: rotate(0);
  }
  0% {
    -webkit-transform: rotate(360deg);
            transform: rotate(360deg);
  }
}
</style>
