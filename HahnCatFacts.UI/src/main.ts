import { createApp } from 'vue';
import App from './presentation/App.vue';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css'; 
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import { CatFactRepository } from '@/data/repositories/CatFactRepository';

ModuleRegistry.registerModules([AllCommunityModule]);
const app = createApp(App);
app.provide('catFactRepository', new CatFactRepository());
app.mount('#app');
