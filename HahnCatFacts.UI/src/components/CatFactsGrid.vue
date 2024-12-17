<template>
  <div class="grid-container">
    <div class="title-container">
      <h1>Cat Facts</h1>
    </div>
    <ag-grid-vue class="grid" :rowData="rowData" :columnDefs="columnDefs" :defaultColDef="defaultColDef"></ag-grid-vue>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, onUnmounted } from 'vue';
import { AgGridVue } from 'ag-grid-vue3';
import { fetchCatFacts } from '@/application/services/CatFactService';
import { CatFact } from '@/domain/entities/CatFact';

const refreshRateInMs = Number(process.env.VUE_APP_GRID_REFRESH_RATE) || 60000;

export default defineComponent({
  name: 'CatFactGrid',
  components: {
    AgGridVue,
  },
  setup() {
    const rowData = ref<CatFact[]>([]);
    const columnDefs = ref([
      {
        headerName: 'ID',
        field: 'id',
        filter: 'agNumberColumnFilter',
        cellStyle: { textAlign: 'center' }
      },
      {
        headerName: 'Description',
        field: 'description',
        filter: 'agTextColumnFilter',
        flex: 1
      },
    ]);

    const defaultColDef = ref({ sortable: true, filter: true, wrapText: true, autoHeight: true });

    const onGridReady = async () => {
      try {
        rowData.value = await fetchCatFacts();
      } catch (error) {
        console.error('Error fetching cat facts:', error);
      }
    };

    let refreshInterval: number;
    onMounted(() => {
      onGridReady();
      refreshInterval = setInterval(onGridReady, refreshRateInMs);
    });

    onUnmounted(() => {
      clearInterval(refreshInterval);
    });

    return { rowData, columnDefs, defaultColDef };
  },
});
</script>

<style scoped>
.grid-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 95vh;
}

.title-container {
  font-family: 'Courier New', Courier, monospace;
  margin-bottom: 20px;
}

.grid {
  background-color: aliceblue;
  height: 50vh;
  width: 80%;
}
</style>
