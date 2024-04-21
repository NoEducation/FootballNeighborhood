import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class CurrentUserService{

    setCurrentUser(token: string, userId : number) : void{
        sessionStorage.setItem('token', token);
        sessionStorage.setItem('userId', userId.toString());
    }

    userIsLogged() : boolean{
        return !!sessionStorage.getItem('userId');
    }

    getCurrentUserId() : number{
        const userId = sessionStorage.getItem('userId');

        if(userId == null) return 0;

        return +userId;
    }
}