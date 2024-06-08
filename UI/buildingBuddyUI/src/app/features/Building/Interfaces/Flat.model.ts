import { Owner } from "./Owner.model";

export interface Flat {
    id: string;
    floor: number;
    roomNumber: string;
    ownerAssigned: Owner;
    totalRooms: number;
    totalBaths: number;
    hasTerrace: boolean;
}