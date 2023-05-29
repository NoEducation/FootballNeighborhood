import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Match } from 'src/app/models/matches/match.model';
import { MatchesService } from 'src/app/sevices/matches-service';
import { MatchDetailsViewMode } from '../models/match-details-view-mode.enum';
import { CreateUpdateMatchRequestBase } from '../models/create-update-match-request-base.model';
import { NotificationService } from 'src/app/sevices/communication/notification.service';
import { NotificationType } from 'src/app/models/common/notification-type.constraint';
import { Location } from '@angular/common'

@Component({
  selector: 'app-match-details',
  templateUrl: './match-details.component.html',
  styleUrls: ['./match-details.component.scss']
})
export class MatchDetailsComponent implements OnInit {


  viewMode : MatchDetailsViewMode = MatchDetailsViewMode.View;

  matchId : number = 0;
  match: Match;

  title: string = '';

  readonly viewModeValues = MatchDetailsViewMode;

  constructor(private readonly activatedRoute : ActivatedRoute,
    private readonly matchService: MatchesService,
    private readonly notificationService: NotificationService,
    private readonly location : Location) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        this.matchId = params['id'];
        this.initializate();
      }
    )
  }

  discardChnages() : void {
    this.loadMatchDetails();
  }

  back() : void{
    this.location.back()
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
          this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
          this.location.back()
        }
      })
    }
    else{
      this.matchService.updateMatch(request).subscribe( {
        next: (response) => {
          this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
          this.location.back()
        }
      })
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
        this.match = response.result.match;
        this.viewMode = MatchDetailsViewMode.View;
        this.setTitle();
      }
    })
  }

  private setTitle() : void{
    this.title = this.viewMode == MatchDetailsViewMode.Add ? 'Dodawanie spotkania' : this.match.name;
  }

}
