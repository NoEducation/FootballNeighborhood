import { GenderEnum } from "src/app/models/user/gender.enum";

export class UpdateCurrentUserRequest{
    userId: number;
    surname: string = "";
    name: string = "";
    email: string = "";
    phone: string = "";
    birthDate?: Date;
    gender?: GenderEnum;
    description: string = "";
}