import { CurrentUserMatchEvaluation } from "./current-user-match-evaluation.model";

export class CurrentUserMatch{
    matchName: string = "";
    city: string = "";
    matchesEvaluations: CurrentUserMatchEvaluation[] = [];
    averageScore: number = 0;
}