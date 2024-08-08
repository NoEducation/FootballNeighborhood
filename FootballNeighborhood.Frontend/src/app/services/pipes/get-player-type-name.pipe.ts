import { Pipe, PipeTransform } from "@angular/core";
import { PlayerType } from "src/app/models/matches/player-type.enum";

@Pipe({
    standalone: true,
    name: 'getPlayerTypeName'
  })
  export class GetPlayerTypeNamePipe implements PipeTransform {
    transform(target: PlayerType): string {
      switch(target) {
        case PlayerType.Playing: return 'Gra';
        case PlayerType.Reserve: return 'Rezerwa';
        default: throw new Error(`PlayerType mapping not added`);
      }
    }
  }