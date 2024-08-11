import { AddMatchPlayerReviewInfo } from "./add-match-player-review-info.model";

export class AddMatchPlayerReviewRequest{
    matchId: number;
    matchReviewScore: number;
    matchOwnerReviewScore: number;
    matchReviewDescription: string;
    finishMatch: boolean;
    playerReviews: Array<AddMatchPlayerReviewInfo>;
}