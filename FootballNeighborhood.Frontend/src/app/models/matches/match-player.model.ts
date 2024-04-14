import { PlayerType } from "./player-type.enum";

export class MatchPlayer{
  matchPlayerId: number;
  userId: number;
  matchId: number;
  playerType: PlayerType;
  userDisplayName: string;
}
