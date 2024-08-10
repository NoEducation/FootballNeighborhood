import { AddMatchPlayerReview } from "./add-match-player-review.model";

export class AddMatchPlayerReviewRequest{
    matchId: number;
    matchReviewScore: number;
    matchReviewDescription: string;
    playerReviews: Array<AddMatchPlayerReview>;
}