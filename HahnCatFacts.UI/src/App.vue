<template>
  <div id="app">
    <CatFactsGrid/>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import CatFactsGrid from './components/CatFactsGrid.vue';

export default defineComponent({
  name: 'App',
  components: {
    CatFactsGrid,
  },
});
</script>

<style>
body{
  background-color: rgb(0, 0, 0, 0.01);
}
</style>
