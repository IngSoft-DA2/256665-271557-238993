import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum"

export interface constructionCompanyAdminCreateRequest 
{
    firstname : string,
    lastname : string,
    email : string,
    password : string,
    invitationId? : string
    userRole? : SystemUserRoleEnum
};