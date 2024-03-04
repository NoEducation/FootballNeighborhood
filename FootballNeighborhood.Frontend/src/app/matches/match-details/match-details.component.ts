import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Match } from 'src/app/models/matches/match.model';
import { MatchesService } from 'src/app/sevices/matches-service';
import { MatchDetailsViewMode } from '../models/match-details-view-mode.enum';
import { CreateUpdateMatchRequestBase } from '../models/create-update-match-request-base.model';
import { NotificationService } from 'src/app/sevices/communication/notification.service';
import { NotificationType } from 'src/app/models/common/notification-type.constraint';
import { Location } from '@angular/common'
import { MatchPlayer } from 'src/app/models/matches/match-player.model';
import { PlayerType } from 'src/app/models/matches/player-type.enum';
import { MatchPlayersService } from 'src/app/sevices/match-players-service';
import { AssignToMatchRequest } from '../models/assign-to-match-request.model';
import { AuthenticationService } from 'src/app/sevices/authentication/authentication.service';

@Component({
  selector: 'app-match-details',
  templateUrl: './match-details.component.html',
  styleUrls: ['./match-details.component.scss']
})
export class MatchDetailsComponent implements OnInit {

  // DODAĆ nowy tryb pogldau owner i user
  viewMode : MatchDetailsViewMode = MatchDetailsViewMode.View;
  
  matchId : number = 0;
  match: Match;
  title: string = '';
  
  alreadyAssigned = false;

  readonly displayedColumns: string[] = ['number', 'userDisplayName', 'playerType', 'actions'];
  readonly viewModeValues = MatchDetailsViewMode;

  constructor(private readonly activatedRoute : ActivatedRoute,
    private readonly matchService: MatchesService,
    private readonly notificationService: NotificationService,
    private readonly authenticationService: AuthenticationService,
    private readonly location : Location,
    private readonly matchPlayersService: MatchPlayersService) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        this.matchId = params['id'];
        this.initializate();
      }
    )
  }

  discardChanges() : void {
    this.loadMatchDetails();
  }

  back() : void{
    this.location.back()
  }

  assingToMatch() : void{
    const request: AssignToMatchRequest = {
      matchId : this.matchId 
    }; 

    this.matchPlayersService.assignToMatch(request).subscribe({
      next: (response) => {
        this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
        this.loadMatchDetails();
      }
    });
  }

  edit() : void{
    // Admin lub super user trzeba dodać !
    this.viewMode = this.viewModeValues.Edit;
  }

  delete(): void{
    this.matchService.removeMatch(this.matchId).subscribe( {
      next: (response) => {
        this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
        this.location.back()
      }
    });
  }

  saveChanges() : void {

    let request : CreateUpdateMatchRequestBase= {
      matchId: this.matchId,
      name : this.match.name,
      startDateTime: this.match.startDateTime,
      endDateTime:  this.match.endDateTime,
      city:  this.match.city,
      addressLine:  this.match.addressLine,
      allowedPlayers: this.match.allowedPlayers,
      minPlayers:  this.match.minPlayers,
      showEmailAddress:  this.match.showEmailAddress,
      showPhoneNumber: this.match.showPhoneNumber,
    }

    if(this.matchId == 0){
      this.matchService.createMatch(request).subscribe( {
        next: (response) => {
          this.matchId = response.result.objectId;
          this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
          this.loadMatchDetails();
        }
      });
    }
    else{
      this.matchService.updateMatch(request).subscribe( {
        next: (response) => {
          this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
          this.loadMatchDetails()        }
      });
    }
  }

  private initializate() : void {
    if(this.matchId && this.matchId != 0) {
      this.loadMatchDetails();
    }
    else{
      this.viewMode = MatchDetailsViewMode.Add;
      this.match = new Match();
      this.setTitle();
    }
  }

  private loadMatchDetails() : void {
    this.matchService.getMatchById(this.matchId).subscribe({
      next: (response) =>{
        this.mapResponse(response.result.match);
        this.viewMode = MatchDetailsViewMode.View;
        this.setTitle();
      }
    });
  }

  private setTitle() : void{
    this.title = this.viewMode == MatchDetailsViewMode.Add ? 'Dodawanie spotkania' : this.match.name;
  }

  private mapResponse(match : Match) : void{
    this.match = match;

    const userId = this.authenticationService.getUserId();

    this.alreadyAssigned = !!this.match.matchPlayers.find(player => player.userId == userId);
  }
}
