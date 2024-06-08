import { Owner } from "../../owner/interfaces/owner";

export interface Flat 
{
    id : string,
    buildingId : string,
    floor : number,
    roomNumber : string,
    ownerAssigned : Owner,
    ownerId : string,
    totalRooms : number,
    totalBath : number,
    hasTerrace : boolean
}


