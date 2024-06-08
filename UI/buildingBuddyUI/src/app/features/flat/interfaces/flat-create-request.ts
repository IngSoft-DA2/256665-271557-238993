import { Owner } from "../../owner/interfaces/owner";

export interface FlatCreateRequest 
{
    floor : number,
    roomNumber : string,
    ownerAssignedId? : string,
    totalRooms : number,
    totalBaths : number,
    hasTerrace : boolean

}
