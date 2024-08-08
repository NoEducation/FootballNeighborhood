
import { RolesEnum } from "src/app/models/common/roles.enum";
import { GenderEnum } from "src/app/models/user/gender.enum";
import { CurrentUserMatch } from "./current-user-match.model";

export class GetCurrentUserReponse{
    surname: string = "";
    name: string = "";
    email: string = "";
    role!: RolesEnum;
    phone: string = "";
    birthDate?: Date;
    gender?: GenderEnum;
    description: string = "";
    matches: Array<CurrentUserMatch> = []
}