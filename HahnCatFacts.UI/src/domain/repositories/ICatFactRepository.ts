import { CatFact } from "../entities/CatFact";

export interface ICatFactRepository {
    getAllCatFacts(): Promise<CatFact[]>;
}