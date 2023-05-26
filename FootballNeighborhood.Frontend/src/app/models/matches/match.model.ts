import { MatchPlayer } from "./match-player.model";

export class Match{

    matchId : number;
    ownerId : number;
    ownerDisplayName : string;
    name: string;
    isFinished: boolean;
    startDateTime: Date;
    EndDateTime: Date;
    City: string;
    AddressLine: string;
    AllowedPlayers: number;
    MinPlayers: number;
    ShowEmailAddress: number;
    ShowPhoneNumber: number;
    MatchPlayers: Array<MatchPlayer>;
}
