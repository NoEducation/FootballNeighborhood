import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { GetAllMatchesResponse } from "../matches/models/get-all-matches-response.model";
import { OperationResult } from "../models/infrastructure/operation-result.model";
import { GetAvailableMatchesByCityResponse } from "../matches/models/get-available-matches-by-city-response.model";
import { SuccessMessage } from "../models/infrastructure/success-message.model";
import { SuccessMessageAndObjectId } from "../models/infrastructure/success-message-and-object-id.model";
import { GetMatchByIdResponse } from "../matches/models/get-match-by-id-response.model";
import { CreateUpdateMatchRequestBase } from "../matches/models/create-update-match-request-base.model";

@Injectable({
  providedIn: 'root'
})
export class MatchesService{
  url = environment.apiUrl + 'Matches/';

  constructor(private readonly http : HttpClient){

  }

  getAllMatches() : Observable<OperationResult<GetAllMatchesResponse>> {
    return this.http.get<OperationResult<GetAllMatchesResponse>>(this.url + 'getAllMatches');
  }

  getAvailableMatchesByCity(city : string) : Observable<OperationResult<GetAvailableMatchesByCityResponse>> {
    return this.http.get<OperationResult<GetAvailableMatchesByCityResponse>>(this.url + 'getAvailableMatchesByCity?city=' + city);
  }

  getUpcomingMatches(userId : number | any = undefined) : Observable<OperationResult<GetAvailableMatchesByCityResponse>> {
    let params = new HttpParams();
    if(userId){
      params = params.append('userId' ,userId)
    }

    return this.http.get<OperationResult<GetAvailableMatchesByCityResponse>>(this.url + 'getUpcomingMatches', { params});
  }

  getMatchById(matchId: number) : Observable<OperationResult<GetMatchByIdResponse>>{
    return this.http.get<OperationResult<GetMatchByIdResponse>>(this.url + 'getMatchById?matchId=' + matchId);
  }

  createMatch(request : CreateUpdateMatchRequestBase) : Observable<OperationResult<SuccessMessageAndObjectId>>{
    return this.http.post<OperationResult<SuccessMessageAndObjectId>>(this.url + 'createMatch', request);
  }

  updateMatch(request : CreateUpdateMatchRequestBase) : Observable<OperationResult<SuccessMessage>>{
    return this.http.post<OperationResult<SuccessMessage>>(this.url + 'updateMatch', request);
  }

  removeMatch(matchId: number) : Observable<OperationResult<SuccessMessage>>{
    return this.http.post<OperationResult<SuccessMessage>>(this.url + 'removeMatch', matchId);
  }

}
