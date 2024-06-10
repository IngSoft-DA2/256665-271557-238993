import { ConstructionCompany } from "../../constructionCompany/interfaces/construction-company";
import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum";

export interface ConstructionCompanyAdmin
{
    id : string,
    firstname : string,
    lastname : string,
    email : string,
    password : string,
    role : SystemUserRoleEnum.ConstructionCompanyAdmin,
    constructionCompany? : ConstructionCompany
}