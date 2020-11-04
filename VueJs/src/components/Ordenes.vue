<template>
  <v-data-table :headers="headers" :items="ordenes" sort-by="calories" class="elevation-1">
    <template v-slot:top>
      <v-toolbar flat color="white">
        <v-toolbar-title>Ordenes</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
       
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" persistent max-width="800px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark class="mb-2" v-on="on">Nueva Orden</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                     <v-select v-model="idCliente" :items="clientes" label="Cliente"></v-select>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                      <v-select v-model="idLibro" :items="libros" label="Libro"></v-select>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="fechaOrden" label="Fecha de la orden"></v-text-field>
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
        { text: 'Nombre del Cliente', value: 'idCliente', sortable: false, filterable: false },
        { text: 'Nombre del Libro', value: 'idLibro', sortable: false, filterable: false },
        //{ text: 'Nombre del Cliente', value: 'nombre', sortable: false, filterable: false },
        //{ text: 'Nombre del Libro', value: 'titulo', sortable: false, filterable: false },
        { text: 'Fecha de la orden', value: 'fechaOrden', sortable: false, filterable: false },
               
      ],
      ordenes: [],  
      clientes: [], 
      libros: [],       
      editedIndex: 0,
      //idCliente: '',
         
      //campos
      idCliente: '', //cliente
      nombre: '',
      idLibro: '', //libros
      titulo: '',

      idOrden: '',      
      titulo: '',
      fechaOrden: '',   

      // variables para modal de activar o desactivar
      adModal: 0,
      adAccion: 0,
      adTitulo: '',
      adIdOrden: '',   

    }),
    computed: {
      formTitle () {
        return this.editedIndex === 0 ? 'Registrar Orden' : 'Editar Orden'
      },
    },
    watch: {
      dialog (val) {
        val || this.close()
      },
    },
    created () {
      this.listar();  
      this.selectCliente();   
      this.selectLibros();
    },
    methods: {
      listar () {
        let me = this;
        let header = { "Authorization" : "Bearer " + this.$store.state.token };
        let configuracion = { headers : header };
        axios.get('api/ordenes', configuracion).then(function(response){
          me.ordenes = response.data;            
         

        }).catch(function(error){
           console.log(error);
        });
        // me.getCliente(me.ordenes);  
      },
      getCliente (ordenes) {
        //this.idOrden = item.id; 
        //this.idCliente = 1;
        let me = this; 
        let header = { "Authorization" : "Bearer " + me.$store.state.token };
        let configuracion = { headers : header };        
         var arrayConfig = [];       
      
        axios.post('api/clientes/buscarPorId', {
               idCliente: me.ordenes.idOrden,         
            }, configuracion).then(function(response){
            
                var respuesta = response.data;
             // this.nombre = respuesta.nombre;
              console.log(respuesta.nombre);  

            }).catch(function(error){
              console.log(error);
              me.dialogMsg = true;
              me.dialogMsgType = 'error';
              me.dialogMsgText = 'Error al eliminar el libro.';
        });          
      },
      selectCliente() {
         let me = this;
         let header = { "Authorization" : "Bearer " + this.$store.state.token };
         let configuracion = { headers : header };
         var clientesArray = [];
         axios.get('api/clientes', configuracion).then(function(response) {
            clientesArray = response.data;
            me.clientes = [];
            
            clientesArray.map(function(x){
               me.clientes.push({ text: x.nombre, value: x.idCliente });
            });
            //console.log("errorwwwwww");
            //console.log(clientesArray);
         }).catch(function(error){
            console.log(error);
         });
      }, 
      selectLibros() {
         let me = this;
         let header = { "Authorization" : "Bearer " + this.$store.state.token };
         let configuracion = { headers : header };
         var librosArray = [];
         axios.get('api/libros', configuracion).then(function(response) {
            librosArray = response.data;
            me.libros = [];
            librosArray.map(function(x){
               me.libros.push({ text: x.titulo, value: x.idLibro });
            });
            //console.log("errorwwwwww");
            //console.log(me.idLibro);
         }).catch(function(error){
            console.log(error);
         });
      },    
      deleteItem (item) {
        this.idOrden = item.idOrden; 
        let me = this; 
        let header = { "Authorization" : "Bearer " + me.$store.state.token };
        let configuracion = { headers : header };      
        var result = confirm("Confirme para eliminar el registro?");
          if (result) { 
          axios.post('api/ordenes/Delete', {
              idOrden: me.idOrden,            
            }, configuracion).then(function(response){
                me.listar();       
            }).catch(function(error){
              console.log(error);
              me.dialogMsg = true;
              me.dialogMsgType = 'error';
              me.dialogMsgText = 'Error al eliminar el libro.';
            });
            }else{
              console.log('ok')
            }
      },
      editItem (item) {        
        this.idOrden = item.idOrden        
        this.idCliente = item.idCliente;
        this.idLibro = item.idLibro;
        this.fechaOrden = item.fechaOrden;  
        this.selectLibros(item.idLibro);
        this.selectCliente(item.idCliente); 
        this.dialog = true;
        console.log(this.idOrden)
        console.log('cliente ' + this.idCliente)
         console.log('libro ' + this.idLibro)
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
        this.idLibro = '';
        this.idCliente = '';
        this.fechaOrden = ''; 
        this.nombre = '';
        this.titulo = ''; 
        this.precio = '';  
        this.id = '';    
      },
      save () {      
        let me = this;
        let header = { "Authorization" : "Bearer " + me.$store.state.token };
        let configuracion = { headers : header };
        if (me.idOrden > 0) {
          console.log('ent')
          console.log(me.idOrden)
          // editar
          axios.put('api/libros/update', {
            idOrden: me.idOrden,
            idCliente: me.idCliente,
            idLibro: me.idLibro,
            fechaOrden: me.fechaOrden,            
          }, configuracion).then(function(response){
            me.close();
            me.listar();
            me.limpiar();            
          }).catch(function(error){
            console.log(error);
            me.dialogMsg = true;
            me.dialogMsgType = 'error';
            me.dialogMsgText = 'Error al actualizar la orden.';
          });
        } else {
          // agregar
           console.log('no ent')
          axios.post('api/ordenes/create', {           
            idCliente: me.idCliente,
            idLibro: me.idLibro,
            fechaOrden: me.fechaOrden,        
          }, configuracion).then(function(response){
            me.close();
            me.listar();
            me.limpiar();            
          }).catch(function(error){
            console.log(error);
            me.dialogMsg = true;
            me.dialogMsgType = 'error';
            me.dialogMsgText = 'Error al crear la orden.';
          });
        }
      },
      activarDesactivarMostrar(accion, item) {
        this.adModal = 1;
        //this.adTitulo = item.titulo;
        this.adIdOrden = item.idOrden;                
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