import { PlayerType } from "./player-type.enum";

export class MatchPlayer{
  userId: number;
  matchId: number;
  playerType: PlayerType;
  userDisplayName: string;
}
