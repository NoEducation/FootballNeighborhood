import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { AddMatchPlayerReviewRequest } from "../matches/models/add-match-player-review-request.model";
import { OperationResult } from "../models/infrastructure/operation-result.model";
import { SuccessMessage } from "../models/infrastructure/success-message.model";
import { GetMatchPlayerReviewsResponse } from "../matches/models/get-match-player-reviews-response.model";

@Injectable({
    providedIn: 'root'
})
export class MatchPlayerReviewService{
    url = environment.apiUrl + 'MatchPlayerReviews/';

    constructor(private readonly http : HttpClient){}

    addMatchPlayerReview(request : AddMatchPlayerReviewRequest) : Observable<OperationResult<SuccessMessage>>{
        return this.http.post<OperationResult<SuccessMessage>>(this.url + 'addMatchPlayerReview', request);
    }

    getMatchPlayerReviews(matchId: number) : Observable<OperationResult<GetMatchPlayerReviewsResponse>>{
        return this.http.get<OperationResult<GetMatchPlayerReviewsResponse>>(this.url + 'getMatchPlayerReviews&matchId=' + matchId);
    }
}