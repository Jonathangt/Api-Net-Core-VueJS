<template>
   <v-app id="inspire">
      <v-navigation-drawer v-model="drawer" :clipped="$vuetify.breakpoint.lgAndUp" app>
         <v-list dense>
            <template >
               <v-list-item :to="{ name: 'home'}">
                  <v-list-item-action>
                     <v-icon>home</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>
                     Inicio
                  </v-list-item-title>
               </v-list-item>
            </template>
            <template>
               <v-list-group>
                  <v-list-item slot="activator">
                     <v-list-item-content>
                        <v-list-item-title>
                           Menu
                        </v-list-item-title>
                     </v-list-item-content>
                  </v-list-item>
                  <v-list-item :to="{ name: 'clientes'}">
                     <v-list-item-action>
                        <v-icon>person</v-icon>
                     </v-list-item-action>
                     <v-list-item-content>
                        <v-list-item-title>
                           Cliente
                        </v-list-item-title>
                     </v-list-item-content>
                  </v-list-item>
                  <v-list-item :to="{ name: 'libros'}">
                     <v-list-item-action>
                        <v-icon>book</v-icon>
                     </v-list-item-action>
                     <v-list-item-content>
                        <v-list-item-title>
                           Libros
                        </v-list-item-title>
                     </v-list-item-content>
                  </v-list-item>
                  <v-list-item :to="{ name: 'ordenes'}">
                     <v-list-item-action>
                        <v-icon>widgets</v-icon>
                     </v-list-item-action>
                     <v-list-item-content>
                        <v-list-item-title>
                           Ordenes
                        </v-list-item-title>
                     </v-list-item-content>
                  </v-list-item>                 
               </v-list-group>
            </template>
         </v-list>
      </v-navigation-drawer>
      <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" app color="blue darken-3" dark>
         <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
         <v-toolbar-title style="width: 300px" class="ml-0 pl-4">
            <span class="hidden-sm-and-down">Libros App</span>
         </v-toolbar-title>
         <v-spacer />
      </v-app-bar>
      <v-content>
         <v-container fluid>
            <v-slide-y-transition mode="out-in">
               <router-view/>
            </v-slide-y-transition>
         </v-container>
      </v-content>
   </v-app>
</template>

<script>
  export default {
    name: 'App',
    data () {
      return {
        clipped: false,
        drawer: true,
        fixed: false,
        items: [{
          icon: 'bubble_chart',
          title: 'Inspire'
        }],
        miniVariant: false,
        right: true,
        rightDrawer: false,
        title: 'Vuetify.js'
      }
    }
  }
</script>