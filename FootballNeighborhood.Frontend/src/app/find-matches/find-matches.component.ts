import { Component, OnInit } from '@angular/core';
import { MatchesService } from '../sevices/matches-service';
import { Router } from '@angular/router';
import { Match } from '../models/matches/match.model';
import { debounceTime } from 'rxjs';
import { Location } from '@angular/common';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@Component({
  selector: 'app-find-matches',
  templateUrl: './find-matches.component.html',
  styleUrls: ['./find-matches.component.scss'],
})
export class FindMatchesComponent implements OnInit {

  city: string;
  availableMatches: Array<Match>;
  cityNotification : string;

  loading = false;

  readonly displayedColumns: string[] = ['name', 'ownerDisplayName', 'city', 'date','startTime', 'endTime', 'addressLine', 'actions'];

  constructor(private readonly matchesService : MatchesService,
    private readonly router: Router,
    private readonly location: Location,
    ) { }

  ngOnInit() {
  }

  cityChanged($event: any) : void {
    debugger;

    if(this.city){
      this.loading = true;
      this.matchesService.getAvailableMatchesByCity(this.city).pipe(
        debounceTime(1000)
      ).subscribe({
        next: (response) => {
          this.availableMatches = response.result.matches;
          this.loading = false;
          this.cityNotification = this.city;
        }
      })
    }
    else{
      this.availableMatches = [];
    }
  }

  assign(matchId : number) : void {

  }


  
  back() : void{
    this.location.back()
  }

  details(matchId: number) : void{
    this.router.navigateByUrl(`matches/matchDetails/${matchId}`);
  }

}
