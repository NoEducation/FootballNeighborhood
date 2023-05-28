import { MatchPlayer } from "./match-player.model";

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
    matchPlayers: Array<MatchPlayer>;
}
