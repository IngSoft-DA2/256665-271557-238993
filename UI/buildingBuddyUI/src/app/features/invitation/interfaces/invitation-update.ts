import { StatusEnum } from "./enums/status-enum";

export interface invitationUpdateRequest
{
    status : StatusEnum,
    expirationDate : Date
};
