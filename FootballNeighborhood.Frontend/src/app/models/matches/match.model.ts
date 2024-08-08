import { MatchPlayer } from "./match-player.model";
import { PlayerMatchStatusEnum } from "./player-match-status-enum";

export class Match{
    matchId : number;
    ownerId : number;
    ownerDisplayName : string;
    name: string;
    isFinished: boolean;
    startDateTime: Date;
    endDateTime: Date;
    city: string;
    addressLine: string;
    allowedPlayers: number;
    minPlayers: number;
    showEmailAddress: boolean;
    showPhoneNumber: boolean;
    playerMatchStatus: PlayerMatchStatusEnum;
    matchPlayers: Array<MatchPlayer>;
}
