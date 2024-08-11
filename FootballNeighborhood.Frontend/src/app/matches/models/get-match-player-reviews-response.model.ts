import { GetMatchPlayerReviewInfo } from "./get-match-player-review-info.model";

export class GetMatchPlayerReviewsResponse{
    matchReviewScore: number;
    matchOwnerReviewScore: number;
    matchReviewDescription: string;
    playerReviews: Array<GetMatchPlayerReviewInfo>;
}