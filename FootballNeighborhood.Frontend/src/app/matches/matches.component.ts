import { Component, OnInit } from '@angular/core';
import { Match } from '../models/matches/match.model';
import { MatchesService } from '../services/matches-service';
import { Router } from '@angular/router';
import { CurrentUserService } from '../services/current-user.service';
import * as moment from 'moment';
import { PlayerMatchStatusEnum } from '../models/matches/player-match-status-enum';
import { MatDialog } from '@angular/material/dialog';
import { MatchScoreDialogComponent } from './match-score-dialog/match-score-dialog.component';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.scss']
})
export class MatchesComponent implements OnInit {

  readonly displayedColumns: string[] = [ 'status', 'name', 'ownerDisplayName', 'city', 'date','startTime', 'endTime', 'addressLine', 'actions'];
  readonly organizedColumns: string[] = [ 'status', 'name', 'date','startTime', 'endTime', 'addressLine', 'playersNumber', 'actions' ];

  matches: Array<Match> = [];
  organizedMatches: Array<Match> = [];
  isMatchOrganiser = false;

  readonly matchPlayerStatusValues = PlayerMatchStatusEnum; 

  constructor(private readonly matchesService : MatchesService,
    private readonly currentUserService: CurrentUserService,
    private readonly router: Router,
    private readonly dialog: MatDialog) { }

  ngOnInit() {
    this.isMatchOrganiser = this.currentUserService.isMatchOrganizer();

    this.matchesService.getUserAssingedMatches().subscribe({
      next: (response) => {
        this.matches = response.result.matches;
      }
    });

    if(this.isMatchOrganiser){
      this.matchesService.getOrganizedMatches().subscribe({
        next: (response) => {
          this.organizedMatches = response.result.matches;
        }
      });
    }
  }

  addNewMatch() : void {
    this.router.navigateByUrl('matches/matchDetails/0');
  }

  findMatches(): void {
    this.router.navigateByUrl('find-matches');
  }

  details(matchId: number) : void{
    this.router.navigateByUrl(`matches/matchDetails/${matchId}`);
  }

  reviewMatch(match: Match) : void{
    const dialog = this.dialog.open(MatchScoreDialogComponent, {
      height: '50rem',
      width: '50rem',
      data: match
    });
  }

  completeMatch() : void{

  }

  getStatus(match: Match) : string{
    switch(match.playerMatchStatus){
      case PlayerMatchStatusEnum.Upcoming: return "Nadchodzące spotkanie";
      case PlayerMatchStatusEnum.Ongoing: return "Spotkanie trwa";
      case PlayerMatchStatusEnum.Expired: return "Spotkanie zakończone ";
      case PlayerMatchStatusEnum.Completed: return "Spotkanie zakończone przez organizatora";
      case PlayerMatchStatusEnum.Reviewed: return "Spotkanie ocenione";
      default: return '';
    }
  }

  getOrganizedStatus(match: Match) : string{
    switch(match.playerMatchStatus){
      case PlayerMatchStatusEnum.Upcoming: return "Nadchodzące spotkanie";
      case PlayerMatchStatusEnum.Ongoing: return "Spotkanie trwa";
      case PlayerMatchStatusEnum.Expired: return "Spotkanie po upływie";
      case PlayerMatchStatusEnum.Completed: return "Potwierdzone zakończone pomyślnie";
      default: return '';
    }
  }

}
