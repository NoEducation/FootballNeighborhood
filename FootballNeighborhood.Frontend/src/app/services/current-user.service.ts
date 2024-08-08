import { Injectable } from "@angular/core";
import { RolesEnum } from "../models/common/roles.enum";

@Injectable({
    providedIn: 'root'
})
export class CurrentUserService{

    setCurrentUser(token: string, userId : number, role: RolesEnum) : void{
        sessionStorage.setItem('token', token);
        sessionStorage.setItem('userId', userId.toString());
        sessionStorage.setItem('role', RolesEnum[role]);
    }

    userIsLogged() : boolean{
        return !!sessionStorage.getItem('userId');
    }

    getCurrentUserId() : number{
        const userId = sessionStorage.getItem('userId');

        if(userId == null) return 0;

        return +userId;
    }

    isMatchOrganizer() : boolean{
        const roleString =  sessionStorage.getItem('role') as string;

        if(!roleString) return false

        return RolesEnum[roleString as keyof typeof RolesEnum] == RolesEnum.MatchOrganizer;
    }

    isPlayer() : boolean{
        const roleString =  sessionStorage.getItem('role') as string;

        if(!roleString) return false

        return RolesEnum[roleString as keyof typeof RolesEnum] == RolesEnum.Player;
    }

    isAdmin() : boolean{
        const roleString =  sessionStorage.getItem('role') as string;

        if(!roleString) return false

        return RolesEnum[roleString as keyof typeof RolesEnum] == RolesEnum.Admin;
    }
}