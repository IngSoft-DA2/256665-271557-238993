import { Flat } from "./Flat.model";

export interface Building {
    id: string;
    name: string;
    managerId: string;
    flats: Flat[];

}