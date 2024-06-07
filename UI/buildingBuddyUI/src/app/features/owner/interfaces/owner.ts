import { Flat } from "../../flat/interfaces/flat"

export interface Owner 
{
    id : string,
    firstname : string,
    lastname : string,
    email : string,
    password : string
    flats : Flat[]
}