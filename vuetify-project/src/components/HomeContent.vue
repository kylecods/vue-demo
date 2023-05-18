<template>
    <v-alert
      v-if="showSuccessAlert"
      closable
      dismissible
      type="success"
      border="left"
      elevation="2"
    >
      {{ alertMessage }}
    </v-alert>

    <v-alert
      v-if="showFailAlert"
      closable
      dismissible
      type="error"
      border="left"
      elevation="2"
    >
      {{ alertMessage }}
    </v-alert>
  <div>
  <v-table v-if="books">
    <thead>
      <tr>
        <th class="text-left">
          Title
        </th>
        <th class="text-left">
          Category
        </th>
        <th class="text-left">
          Description
        </th>
        <th class="text-left">
          CreatedDate
        </th>
        <th class="text-left">
          Actions
        </th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="item in books.items"
        :key="item.title"
      >
        <td>{{ item.title }}</td>
        <td>{{ item.category }}</td>
        <td>{{ item.description }}</td>
        <td>{{ item.createdDate }}</td>
        <td>
            <v-btn icon="mdi-pencil" color="primary" @click="openEditModal">
            </v-btn>
            <v-btn icon="mdi-delete" color="error" @click="deleteItem(item)">
            </v-btn>
          </td>
      </tr>
    </tbody>
  </v-table>

  <v-dialog v-model="editModalOpen" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">Edit Item</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="editForm">
            <v-text-field v-model="editedItem.title" label="Title"></v-text-field>
            <v-text-field v-model="editedItem.category" label="Category"></v-text-field>
            <v-text-field v-model="editedItem.description" label="Description"></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" @click="saveEdit">Edit</v-btn>
          <v-btn color="error" @click="cancelEdit">Cancel</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Add Item Modal -->
    <v-dialog v-model="addModalOpen" max-width="500px">
      <template v-slot:activator="{ on }">
        <v-btn color="primary" @click="openAddModal" v-bind="on">
          Add Item
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="headline">Add Item</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="addForm">
            <v-text-field  :rules="[v => !!v || 'Title is required']" v-model="newItem.title" label="Title"></v-text-field>
            <v-text-field  :rules="[v => !!v || 'Author name is required']" v-model="newItem.authorName" label="Author"></v-text-field>
            <v-text-field  :rules="[v => !!v || 'Category is required']" v-model="newItem.category" label="Category"></v-text-field>
            <v-text-field  :rules="[v => !!v || 'Description is required']" v-model="newItem.description" label="Description"></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" @click="saveAdd">Create</v-btn>
          <v-btn color="error" @click="cancelAdd">Cancel</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>

  </template>
  
<script>

export default {
  data: () => ({
    books: null,
    editModalOpen: false,
    addModalOpen: false,
    editedItem: {
      title: '',
      authorName:'',
      category: '',
      description: '',
    },
    newItem: {
      title: '',
      authorName:'',
      category: '',
      description: '',
    },
    showSuccessAlert: false,
    showFailAlert: false,
    alertMessage: '',
  }),

  created() {
    // fetch on init
    this.fetchData()
  },

  methods: {
    async fetchData() {
      const FETCH_BOOKS_URL = `http://localhost:5154/bookshelf/all-books`
      try {
        const response = await fetch(FETCH_BOOKS_URL);
        this.books = await response.json();
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    },
    openEditModal() {
      // Assign the selected item's values to the editedItem object
      // based on the selected item's index or ID
      const selectedItem = this.books.items[0]; // Replace with the actual selected item
      this.editedItem = { ...selectedItem };
      this.editModalOpen = true;
    },
    openAddModal() {
      // Clear the newItem object
      this.newItem = { title: '', category: '', description: '' };
      this.addModalOpen = true;
    },
    async saveAdd() {
      if (this.$refs.addForm.validate()) {
        // Perform logic to save the new item

        try {
          const ADD_URL = "http://localhost:5154/bookshelf/create-book";

          const response = await fetch(ADD_URL, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.newItem),
          });
          
          if (response.ok) {
            this.newItem = { title: '',authorName:'', category: '', description: '' };
            this.addModalOpen = false;
            this.fetchData();
            this.showSuccessAlert = true
            this.alertMessage = "Book added successfully!"
          }
        } catch (error) {
          console.error('Error adding book:', error);
          this.fetchData();
          this.showAlert = true;
          this.alertMessage = "An error has occurred."
        }
      }
    },

    // Cancel adding and close the modal
    cancelAdd() {
      // Clear the newItem object and close the modal
      this.newItem = { title: '', category: '', description: '' };
      this.addModalOpen = false;
    },

    async saveEdit() {
      if (this.$refs.editForm.validate()) {
        // Perform logic to save the new item

        try {
          const EDIT_URL = "http://localhost:5154/bookshelf/update-book";

          const response = await fetch(EDIT_URL, {
            method: 'PATCH',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.editedItem),
          });
          
          if (response.ok) {
            this.editedItem = { title: '',authorName:'', category: '', description: '' };
            this.addModalOpen = false;
            this.fetchData();
            this.showSuccessAlert = true;
            this.alertMessage = "Book edited successfully!"
          }
        } catch (error) {
          console.error('Error updating book:', error);
          this.fetchData();
          this.showFailAlert = true;
          this.alertMessage = "An error has occurred."
        }
      }
    },
    cancelEdit() {
      // Clear the newItem object and close the modal
      this.editItem = { title: '', category: '', description: '' };
      this.addModalOpen = false;
    },
    async deleteItem(item) {
      const DELETE_URL = "http://localhost:5154/bookshelf/delete-book";
      try {
        const response = await fetch(`${DELETE_URL}?id=${item.id}`, {
          method: 'DELETE',
        });
        if (response.ok) {
          this.fetchData();
          this.showSuccessAlert = true;
          this.alertMessage = "Book deleted successfully!"
        }
      } catch (error) {
        console.error('Error deleting item:', error);
        this.fetchData();
        this.showFailAlert = true;
        this.alertMessage = "An error has occurred."
      }
    },
  }
}

  </script>