export class CreateUpdateMatchRequestBase{
  matchId: number;
  name : string;
  startDateTime: Date;
  endDateTime: Date;
  city: string;
  addressLine: string;
  allowedPlayers: number;
  minPlayers: number;
  showEmailAddress: boolean;
  showPhoneNumber: boolean;
}
