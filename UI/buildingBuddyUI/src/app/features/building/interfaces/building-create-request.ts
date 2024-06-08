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

