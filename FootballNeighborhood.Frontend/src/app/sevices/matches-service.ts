import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { GetAllMatchesResponse } from "../matches/models/get-all-matches-response.model";
import { OperationResult } from "../models/infrastructure/operation-result.model";
import { GetAvailableMatchesByCityResponse } from "../matches/models/get-available-matches-by-city-response.model";

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
    return this.http.get<OperationResult<GetAvailableMatchesByCityResponse>>(this.url + 'getAvailableMatchesByCity?' + city);
  }

}
