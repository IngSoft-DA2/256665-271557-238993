import { FlatCreateRequest } from "../../flat/interfaces/flat-create-request";
import { LocationRequest } from "./location/location-request";

export interface BuildingCreateRequest 
{
    managerId : string,
    name : string,
    address : string,
    location : LocationRequest,
    constructionCompanyId : string,
    commonExpenses : number,
    flats : FlatCreateRequest[]
}

