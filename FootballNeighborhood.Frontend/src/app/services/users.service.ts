import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CheckUserHasActiveConfirmationResponse } from '../confirmation-user/models/check-user-has-active-confirmation-response.model';
import { OperationResult } from '../models/infrastructure/operation-result.model';
import { GetCurrentUserReponse } from '../profile/model/get-current-user-response.model';
import { UpdateCurrentUserRequest } from '../profile/model/update-current-user-request.model';
import { SuccessMessage } from '../models/infrastructure/success-message.model';


@Injectable({
    providedIn: 'root'
})
export class UsersService {

    url = environment.apiUrl + 'Users/';

    constructor(private readonly http : HttpClient)
    {}

    getCurrentUser(userId: number): Observable<OperationResult<GetCurrentUserReponse>> {
        return this.http.get<OperationResult<GetCurrentUserReponse>>(`${this.url}currentUser?userId=${userId}`);
    }

    updateCurrentUser(request: UpdateCurrentUserRequest): Observable<OperationResult<SuccessMessage>> {
        return this.http.put<OperationResult<SuccessMessage>>(`${this.url}currentUser`, request);
    }
}
