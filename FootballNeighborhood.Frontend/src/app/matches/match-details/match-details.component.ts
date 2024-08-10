import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationType } from 'src/app/models/common/notification-type.constraint';
import { Match } from 'src/app/models/matches/match.model';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { NotificationService } from 'src/app/services/communication/notification.service';
import { MatchPlayersService } from 'src/app/services/match-players-service';
import { MatchesService } from 'src/app/services/matches-service';
import { AssignToMatchRequest } from '../models/assign-to-match-request.model';
import { CreateUpdateMatchRequestBase } from '../models/create-update-match-request-base.model';
import { ComponentViewModeEnum } from '../../models/common/component-view-mode.enum';
import { PlayerType } from 'src/app/models/matches/player-type.enum';
import { CurrentUserService } from 'src/app/services/current-user.service';

@Component({
  selector: 'app-match-details',
  templateUrl: './match-details.component.html',
  styleUrls: ['./match-details.component.scss']
})
export class MatchDetailsComponent implements OnInit {

  //TODO.DA DODAĆ nowy tryb pogldau owner i user
  viewMode : ComponentViewModeEnum = ComponentViewModeEnum.View;
  
  matchId : number = 0;
  match: Match;
  title: string = '';
  isOrganizer = false;

  
  alreadyAssigned = false;
  isUserOrganizer = false;

  readonly displayedColumns: string[] = ['number', 'userDisplayName', 'playerType', 'actions'];
  readonly viewModeValues = ComponentViewModeEnum;
  readonly playerTypes = PlayerType;

  constructor(private readonly activatedRoute : ActivatedRoute,
    private readonly matchService: MatchesService,
    private readonly notificationService: NotificationService,
    private readonly authenticationService: AuthenticationService,
    private readonly location : Location,
    private readonly matchPlayersService: MatchPlayersService,
    private readonly currentUserService: CurrentUserService) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        this.matchId = params['id'];
        this.initializate();
      }
    );

    this.isOrganizer = this.currentUserService.isMatchOrganizer();
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

  unsubsriveFromMatch(): void{
    this.removePlayer(this.currentUserService.getCurrentUserId(), this.matchId);
  }

  edit() : void{
    // Admin lub super user trzeba dodać !
    this.viewMode = this.viewModeValues.Edit;
  }

  delete(): void{
    this.matchService.removeMatch(this.matchId).subscribe( {
      next: (response) => {
        this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
        this.location.back();
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
          this.loadMatchDetails();        
        }
      });
    }
  }

  details(userId: number) : void{
    //TODO.DA navigate to user details
  }

  removePlayer(userId: number, matchId: number) : void{
    this.matchPlayersService.unassingFromMatch({
      matchId: matchId,
      userId: userId
    }).subscribe({
      next: (response) => {
        debugger;
        this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
        this.loadMatchDetails();    
      }
    });
  }

  private initializate() : void {
    if(this.matchId && this.matchId != 0) {
      this.loadMatchDetails();
    }
    else{
      this.viewMode = ComponentViewModeEnum.Add;
      this.match = new Match();
      this.setTitle();
    }
  }

  private loadMatchDetails() : void {
    this.matchService.getMatchById(this.matchId).subscribe({
      next: (response) =>{
        this.mapResponse(response.result.match);
        this.viewMode = ComponentViewModeEnum.View;
        this.isUserOrganizer = response.result.match.ownerId == this.currentUserService.getCurrentUserId();
        this.setTitle();
      }
    });
  }

  private setTitle() : void{
    this.title = this.viewMode == ComponentViewModeEnum.Add ? 'Dodawanie spotkania' : this.match.name;
  }

  private mapResponse(match : Match) : void{
    this.match = match;

    const userId = this.authenticationService.getUserId();

    this.alreadyAssigned = !!this.match.matchPlayers.find(player => player.userId == userId);
  }
}
