import { CreateFlatRequest } from "../../flat/interfaces/flat-create-request";
import { LocationRequest } from "./location/location-request";

export interface CreateBuildingRequest 
{
    managerId : string,
    name : string,
    address : string,
    location : LocationRequest,
    constructionCompanyId : string,
    commonExpenses : number,
    flats : CreateFlatRequest[]
}

