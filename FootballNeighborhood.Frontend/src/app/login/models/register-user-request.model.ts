import { Roles } from "src/app/models/common/roles.enum";

export class RegisterUserRequest{
  email: string;
  login: string;
  password: string;
  role: Roles;
}
