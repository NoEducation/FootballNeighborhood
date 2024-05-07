import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { OperationResult } from "../models/infrastructure/operation-result.model";
import { SuccessMessage } from "../models/infrastructure/success-message.model";
import { Observable } from "rxjs";
import { CheckUserHasActiveConfirmationResponse } from "../confirmation-user/models/check-user-has-active-confirmation-response.model";


@Injectable({
    providedIn: 'root'
})
export class UserConfirmationService{
    url = environment.apiUrl + 'Matches/';

    constructor(private readonly http : HttpClient){
    }
    
    isConfirmationActive(userId: number): Observable<OperationResult<CheckUserHasActiveConfirmationResponse>> {
        return this.http.get<OperationResult<CheckUserHasActiveConfirmationResponse>>(`${this.url}?userId=${userId}`);
    }
    
    confirmUser(userId: number, code: string): Observable<OperationResult<SuccessMessage>> {
        const body = {
          userId,
          code
        };
    
        return this.http.post<OperationResult<SuccessMessage>>(`${this.url}/ConfirmUser`, JSON.stringify(body));
    }
}