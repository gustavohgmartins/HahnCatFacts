<template>
    <ag-grid-vue class="grid" :rowData="rowData" :columnDefs="columnDefs" :defaultColDef="defaultColDef"></ag-grid-vue>
</template>

<script lang="ts">
import { defineComponent, inject, ref, onMounted, onUnmounted } from 'vue';
import { AgGridVue } from 'ag-grid-vue3';
import { CatFactRepository } from '@/data/repositories/CatFactRepository';
import { CatFact } from '@/domain/entities/CatFact';

const refreshRateInMs = Number(process.env.VUE_APP_GRID_REFRESH_RATE) || 60000;

export default defineComponent({
  name: 'CatFactGrid',
  components: {
    AgGridVue,
  },
  setup() {
    const catFactRepository = inject<CatFactRepository>('catFactRepository')!;
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
        rowData.value = await catFactRepository.getAllCatFacts();
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
