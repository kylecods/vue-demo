<template>
       <v-app-bar>
        <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
  
        <v-toolbar-title>Book Shelf</v-toolbar-title>
      </v-app-bar>
  
      <v-navigation-drawer
        v-model="drawer"
        temporary
      >
        <v-list nav>
          <v-list-item prepend-icon="mdi-addressbook" title="Books" value="home" href="/"></v-list-item>
        </v-list>
      </v-navigation-drawer>
  
</template>

<script>
    export default {
      data: () => ({ drawer: null }),
    }
</script>
