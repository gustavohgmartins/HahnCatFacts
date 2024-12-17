<template>
  <div id="app">
    <div class="grid-container">
      <h1 class="title">Cat Facts</h1>
      <CatFactsGrid class="grid"/>
    </div>
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
.grid-container{
  width: 100%;
  height: 95vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.title{
  font-family: 'Courier New', Courier, monospace;
}

.grid{
  width: 80%;
  height: 55%;
}
</style>
