import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit, OnDestroy {
  generalLinks: any[];
  otherLinks: any[];
  // subscription: Subscription;
  // dataLoading: boolean;
  // activeDirectoryAuthentication: boolean;
  // azureActiveDirectoryAuthentication: boolean;
  // loggedInEmployeeDisplayName: string;
  // loggedInEmployeeId: number;

  constructor(
    private router: Router,
    // private loadingService: LoadingService,
  ) {
    this.generalLinks = [
      {
        label: 'Dashboard',
        path: './dashboard',
        icon: 'dashboard'
      }, {
        label: 'Cases',
        path: './cases',
        icon: 'work'
      }, {
        label: 'Documents',
        path: './documents',
        icon: 'description'
      }, {
        label: 'Processes',
        path: './processes',
        icon: 'account_tree'
      }, {
        label: 'Employees',
        path: './employees',
        icon: 'group'
      },
    ];
    this.otherLinks = [
      {
        label: 'Configuration',
        path: './configuration',
        icon: 'settings'
      }, {
        label: 'Support',
        path: './logged-in-support',
        icon: 'comment'
      },
    ];
    // this.activeDirectoryAuthentication = JSON.parse(sessionStorage.getItem('adauth')) === true;
    // this.azureActiveDirectoryAuthentication = JSON.parse(sessionStorage.getItem('aadauth')) === true;
  }

   ngOnInit(): void {
    // this.subscription = this.loadingService.loadingState.subscribe((state: LoadingState) => {
    //   this.dataLoading = state.dataLoading;
    // });
    // this.loggedInEmployeeDisplayName = sessionStorage.getItem('loggedInEmployeeDisplayName');
    // this.loggedInEmployeeId = JSON.parse(sessionStorage.getItem('loggedInEmployeeId'));
  }

  displayEmployeeWritePage(): void {
    // this.router.navigate(['/employees/employee_write', this.loggedInEmployeeId]);
  }

  logOut(): void {
    sessionStorage.clear();
    this.router.navigateByUrl('/login');
  }

  navigateToHomePage(): void {
    this.router.navigateByUrl('/home');
  }

   ngOnDestroy(): void {
    // this.subscription.unsubscribe();
  }
}
