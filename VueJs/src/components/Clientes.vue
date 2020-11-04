<template>
  <v-data-table :headers="headers" :items="clientes" sort-by="calories" class="elevation-1">
    <template v-slot:top>
      <v-toolbar flat color="white">
        <v-toolbar-title>Clientes</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
       
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" persistent max-width="800px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo Cliente</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="nombre" label="Nombre"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="apellido" label="Apellido"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="telefono" label="Telefono"></v-text-field>
                  </v-col>                  
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="ciudad" label="Ciudad"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="direccion" label="Dirección"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="estado" label="Estado"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="codigoPostal" label="Código Postal"></v-text-field>
                  </v-col>             
                </v-row>
                <v-row>
                  <v-alert :value="dialogMsg" dense outlined :color="dialogMsgType" transition="scale-transition">
                    {{ dialogMsgText }}
                  </v-alert>
                </v-row>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="red darken-1" text @click="close">Cancelar</v-btn>
              <v-btn color="blue darken-1" text @click="save">Guardar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>      
      </v-toolbar>
    </template>
    <template v-slot:item.opciones="{ item }">
      <v-icon small color="blue darken-1" class="mr-2" @click="editItem(item)">edit</v-icon>   
      <v-icon small color="blue darken-1" class="mr-2" @click="deleteItem(item)">delete</v-icon>   
    </template>  
    
  </v-data-table>
</template>

<script>
  import axios from 'axios'
  export default {
    data: () => ({
      dialog: false,
      dialogMsg : false,  // Para el dialogo de la alerta
      dialogMsgText: '',  // Texto del dialgo de la alerta
      dialogMsgType: '',  // Tipo del dialogo de la alerta
     
      headers: [
        { text: 'Opciones', value: 'opciones', sortable: false, filterable: false },
        { text: 'Nombre', value: 'nombre', sortable: false, filterable: false },
        { text: 'Apellido', value: 'apellido', sortable: false, filterable: false },
        { text: 'Telefono', value: 'telefono', sortable: false, filterable: false },
        { text: 'Direccion', value: 'direccion', sortable: false, filterable: false },
        { text: 'Ciudad', value: 'ciudad', sortable: false, filterable: false },
        { text: 'Estado', value: 'estado', sortable: false, filterable: false },
        { text: 'C. Postal', value: 'codigoPostal', sortable: false, filterable: false },       
      ],
      clientes: [],    
      editedIndex: 0,

      //campos
      idCliente: '',     
      nombre: '',
      apellido: '',
      telefono: '',
      direccion: '',
      ciudad: '',
      estado: '',
      codigoPostal: '',

      // variables para modal de activar o desactivar
      adModal: 0,
      adAccion: 0,
      adNombre: '',
      adIdCliente: '',   
  
      //clientes: '',
    }),
    computed: {
      formTitle () {
        return this.editedIndex === 0 ? 'Registrar Cliente' : 'Editar Cliente'
        
      },
    },
    watch: {
      dialog (val) {
        val || this.close()
      },
    },
    created () {
      this.listar();     
    },
    methods: {
      listar () {
        let me = this;
        let header = { "Authorization" : "Bearer " + this.$store.state.token };
        let configuracion = { headers : header };
        axios.get('api/clientes', configuracion).then(function(response){
          me.clientes = response.data;
        }).catch(function(error){
           console.log("errorrr");
          console.log(error);
        });
      },
      deleteItem (item) {
        this.idCliente = item.idCliente; 
        let me = this; 
        let header = { "Authorization" : "Bearer " + me.$store.state.token };
        let configuracion = { headers : header };      
        var result = confirm("Confirme para eliminar el registro?");
          if (result) { 
          axios.post('api/clientes/Delete', {
               idCliente: me.idCliente,               
          }, configuracion).then(function(response){
               me.listar();    
          }).catch(function(error){
            console.log(error);
            me.dialogMsg = true;
            me.dialogMsgType = 'error';
            me.dialogMsgText = 'Error al eliminar el cliente.';
          });
          }else{
            console.log('ok')
          }
      },
      editItem (item) {
        this.idCliente = item.idCliente
        this.nombre = item.nombre;
        this.apellido = item.apellido;
        this.telefono = item.telefono;
        this.direccion = item.direccion;
        this.ciudad = item.ciudad;
        this.estado = item.estado;
        this.codigoPostal = item.codigoPostal;
        this.dialog = true;
        console.log('ww')
         console.log(this.idCliente)
         console.log(item.id);
      },
      close () {
        this.dialog = false;
        this.dialogMsg = false;
        this.dialogMsgText = '';
        this.dialogMsgType = '';
        this.limpiar();
      },
      limpiar () {
        this.editedIndex = 0;       
        this.idCliente = '';
        this.nombre = '';
        this.apellido = '';
        this.direccion = '';
        this.telefono = '';
        this.ciudad = '';
        this.estado = '';
        this.codigoPostal = '';

      },
      save () {
        let me = this;
        let header = { "Authorization" : "Bearer " + me.$store.state.token };
        let configuracion = { headers : header };
         console.log(me.idCliente);
        if (me.idCliente > 0) {
          console.log('entro');
         
          // editar
          axios.put('api/clientes/update', {
            idCliente: me.idCliente,
            nombre: me.nombre,
            apellido: me.apellido,
            telefono: me.telefono,
            direccion: me.direccion,
            ciudad: me.ciudad,
            estado: me.estado,
            codigoPostal: me.codigoPostal,
          }, configuracion).then(function(response){
            me.close();
            me.listar();
            me.limpiar();            
          }).catch(function(error){
            console.log(error);
            me.dialogMsg = true;
            me.dialogMsgType = 'error';
            me.dialogMsgText = 'Error al actualizar el cliente.';
          });
        } else {
          console.log('no entro');
          // agregar 
          axios.post('api/clientes/create', {
            nombre: me.nombre,
            apellido: me.apellido,
            telefono: me.telefono,
            direccion: me.direccion,
            ciudad: me.ciudad,
            estado: me.estado,
            codigoPostal: me.codigoPostal,
          }, configuracion).then(function(response){
            me.close();
            me.listar();
            me.limpiar();            
          }).catch(function(error){
            console.log(error);
            me.dialogMsg = true;
            me.dialogMsgType = 'error';
            me.dialogMsgText = 'Error al crear el cliente.';
          });
        }
      },
      activarDesactivarMostrar(accion, item) {
        this.adModal = 1;
        this.adNombre = item.nombre;
        this.adIdCliente = item.idCliente;                
        if (accion == 1) {
         this.adAccion = 1;
        } else if (accion == 2) {
          this.adAccion = 2;
        } else {
          this.adModal = 0;
        }
      },
      activarDesactivarCerrar(){
        this.adModal = 0;
      },
        
    },
  }
</script>