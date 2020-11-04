<template>
  <v-data-table :headers="headers" :items="libros" sort-by="calories" class="elevation-1">
    <template v-slot:top>
      <v-toolbar flat color="white">
        <v-toolbar-title>Libros</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
       
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" persistent max-width="800px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo Libro</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="titulo" label="Titulo"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="autorNombre" label="Nombre del autor"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="autorApellido" label="Apellido del autor"></v-text-field>
                  </v-col>                  
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="categoria" label="Categoria"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                     <v-text-field v-model="precio" label="Precio 0.0"></v-text-field>
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
        { text: 'Titulo', value: 'titulo', sortable: false, filterable: false },
        { text: 'Nombre del Autor', value: 'autorNombre', sortable: false, filterable: false },
        { text: 'Apellido del Autor', value: 'autorApellido', sortable: false, filterable: false },
        { text: 'Categoria', value: 'categoria', sortable: false, filterable: false },
        { text: 'Precio', value: 'precio', sortable: false, filterable: false },            
      ],
      libros: [],    
      editedIndex: 0,

      //campos
      idLibro: '',      
      titulo: '',
      autorNombre: '',
      autorApellido: '',
      categoria: '',
      precio: '',   

      // variables para modal de activar o desactivar
      adModal: 0,
      adAccion: 0,
      adTitulo: '',
      adIdLibro: '',   

    }),
    computed: {
      formTitle () {
        return this.editedIndex === 0 ? 'Registrar Libro' : 'Editar Libro'
        console.log(this.editedIndex)
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
        axios.get('api/libros', configuracion).then(function(response){
          me.libros = response.data;
        }).catch(function(error){
           console.log("errorrr");
          console.log(error);
        });
      },
      deleteItem (item) {

        this.idLibro = item.idLibro;      
        let me = this; 
        let header = { "Authorization" : "Bearer " + me.$store.state.token };
        let configuracion = { headers : header }; 
        var result = confirm("Confirme para eliminar el registro?");
          if (result) {     
            axios.post('api/libros/Delete', {
                idLibro: me.idLibro,       
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
        this.idLibro = item.idLibro        
        this.titulo = item.titulo;
        this.autorNombre = item.autorNombre;
        this.autorApellido = item.autorApellido;
        this.categoria = item.categoria;
        this.precio = item.precio;      
        this.dialog = true;
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
        this.titulo = '';
        this.autorNombre = '';
        this.autorApellido = '';
        this.categoria = '';
        this.precio = '';        
      },
      save () {
        let me = this;
        let header = { "Authorization" : "Bearer " + me.$store.state.token };
        let configuracion = { headers : header };
        if (me.idLibro > 0) {
          // editar
          axios.put('api/libros/update', {
            idLibro: me.idLibro,
            titulo: me.titulo,
            autorNombre: me.autorNombre,
            autorApellido: me.autorApellido,
            categoria: me.categoria,
            precio: me.precio,
          }, configuracion).then(function(response){
            me.close();
            me.listar();
            me.limpiar();            
          }).catch(function(error){
            console.log(error);
            me.dialogMsg = true;
            me.dialogMsgType = 'error';
            me.dialogMsgText = 'Error al actualizar el libro.';
          });
        } else {
          // agregar
          axios.post('api/libros/create', {
            titulo: me.titulo,
            autorNombre: me.autorNombre,
            autorApellido: me.autorApellido,
            categoria: me.categoria,
            precio: me.precio,            
          }, configuracion).then(function(response){
            me.close();
            me.listar();
            me.limpiar();            
          }).catch(function(error){
            console.log(error);
            me.dialogMsg = true;
            me.dialogMsgType = 'error';
            me.dialogMsgText = 'Error al crear el libros.';
          });
        }
      },
      activarDesactivarMostrar(accion, item) {
        this.adModal = 1;
        this.adTitulo = item.titulo;
        this.adIdLibro = item.idLibro;                
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