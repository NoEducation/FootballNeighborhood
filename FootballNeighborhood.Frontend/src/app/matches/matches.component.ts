import { Component, OnInit } from '@angular/core';
import { Match } from '../models/matches/match.model';
import { MatchesService } from '../services/matches-service';
import { Router } from '@angular/router';
import { CurrentUserService } from '../services/current-user.service';
import * as moment from 'moment';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.scss']
})
export class MatchesComponent implements OnInit {

  readonly displayedColumns: string[] = ['name', 'status', 'ownerDisplayName', 'city', 'date','startTime', 'endTime', 'addressLine', 'actions'];

  matches: Array<Match> = [];
  organizedMatches: Array<Match> = [];

  isMatchOrganiser = false;

  constructor(private readonly matchesService : MatchesService,
    private readonly currentUserService: CurrentUserService,
    private readonly router: Router) { }

  ngOnInit() {

    this.isMatchOrganiser = this.currentUserService.isMatchOrganizer();

    this.matchesService.getUpcomingMatches().subscribe({
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

  getStatus(match: Match) : string{
    if(this.matchExpired(match)) return 'Spotkanie po upływanie...';
    else return 'Nadchodzące spotkanie'
  }

  matchExpired(match: Match): boolean{
    return moment(match.endDateTime) < moment(new Date())
  }
}
