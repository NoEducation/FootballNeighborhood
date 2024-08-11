import { Match } from "./match.model";
import { OwnerInfo } from "./owner-info.model";

export class MatchDetails extends Match{
    ownerInfo: OwnerInfo;
}