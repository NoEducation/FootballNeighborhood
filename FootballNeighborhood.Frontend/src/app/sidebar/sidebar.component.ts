import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoadingService } from '../sevices/communication/loading.service';
import { LoadingState } from '../sevices/communication/loadingstate.interface';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit, OnDestroy {
  generalLinks: any[];
  otherLinks: any[];
  subscription: Subscription;
  dataLoading: boolean;
  // loggedInEmployeeDisplayName: string;
  // loggedInEmployeeId: number;

  constructor(
    private router: Router,
    private loadingService: LoadingService,
  ) {
    this.generalLinks = [
      {
        label: 'Spoktania',
        path: './matches',
        icon: 'groups'
      },
      {
        label: 'Wyszukaj spotkanie',
        path: './find-matches',
        icon: 'search'
      },
      {
        label: 'Inni gracze',
        path: './players',
        icon: 'dashboard'
      },
    ];
    this.otherLinks = [
      {
        label: 'Profil',
        path: './profil',
        icon: 'dashboard'
      },
      {
        label: 'Pomoc',
        path: './logged-in-support',
        icon: 'comment'
      },
    ];
  }

   ngOnInit(): void {
    this.subscription = this.loadingService.loadingState.subscribe((state: LoadingState) => {
      this.dataLoading = state.dataLoading;
    });
    // this.loggedInEmployeeDisplayName = sessionStorage.getItem('loggedInEmployeeDisplayName');
    // this.loggedInEmployeeId = JSON.parse(sessionStorage.getItem('loggedInEmployeeId'));
  }

  logOut(): void {
    sessionStorage.clear();
    this.router.navigateByUrl('/login');
  }

  navigateToHomePage(): void {
    this.router.navigateByUrl('/home');
  }

   ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
