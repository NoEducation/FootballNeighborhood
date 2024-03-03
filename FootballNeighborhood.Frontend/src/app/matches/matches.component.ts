import { Component, OnInit } from '@angular/core';
import { Match } from '../models/matches/match.model';
import { MatchesService } from '../sevices/matches-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.scss']
})
export class MatchesComponent implements OnInit {

  readonly displayedColumns: string[] = ['name', 'ownerDisplayName', 'city', 'date','startTime', 'endTime', 'addressLine', 'actions'];

  matches: Array<Match> = [];

  constructor(private readonly matchesService : MatchesService,
    private readonly router: Router) { }

  ngOnInit() {
    this.matchesService.getUpcomingMatches().subscribe({
      next: (response) => {
        this.matches = response.result.matches;
      }
    })
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
}
