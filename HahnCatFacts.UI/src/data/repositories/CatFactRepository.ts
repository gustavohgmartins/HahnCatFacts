import { CatFact } from "@/domain/entities/CatFact";
import { ICatFactRepository } from "@/domain/repositories/ICatFactRepository";
import axios from "axios";

const apiUrl = process.env.VUE_APP_API_URL + "/CatFact";

export class CatFactRepository implements ICatFactRepository {
    async getAllCatFacts(): Promise<CatFact[]> {
        const response = await axios.get<CatFact[]>(apiUrl); 
        
        return response.data;
    }
}