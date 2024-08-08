import { RolesEnum } from "src/app/models/common/roles.enum";

export class UserLogged{
  token: string;
  userId: number;
  role: RolesEnum;
}
