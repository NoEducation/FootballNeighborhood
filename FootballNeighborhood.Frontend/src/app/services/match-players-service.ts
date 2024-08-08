import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { OperationResult } from "../models/infrastructure/operation-result.model";
import { Observable } from "rxjs";
import { SuccessMessage } from "../models/infrastructure/success-message.model";
import { UnassingFromMatchRequest } from "../matches/models/unassing-from-match-request.model";
import { AssignToMatchRequest } from "../matches/models/assign-to-match-request.model";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class MatchPlayersService{
    url = environment.apiUrl + 'MatchPlayers/';

    constructor(private readonly http : HttpClient){
    }

    assignToMatch(request : AssignToMatchRequest) : Observable<OperationResult<SuccessMessage>>{
        return this.http.post<OperationResult<SuccessMessage>>(this.url + 'assingToMatch', request);
    }
    
    unassingFromMatch(request: UnassingFromMatchRequest) : Observable<OperationResult<SuccessMessage>>{
        return this.http.post<OperationResult<SuccessMessage>>(this.url + 'unassignFromMatch', request);
    }

}