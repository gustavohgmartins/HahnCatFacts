import { CatFact } from '@/domain/entities/CatFact';
import axios from 'axios';

const apiUrl = process.env.VUE_APP_API_URL + "/CatFact";

export async function fetchCatFacts(): Promise<CatFact[]> 
{ 
    const response = await axios.get<CatFact[]>(apiUrl); 
    
    return response.data;
}