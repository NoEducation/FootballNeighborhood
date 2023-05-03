import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  readonly routesOutOfLayout: string[] = [
    '/login',
    '/confirmation-user',
    '/support',
    '/privacy-policy'
  ];


  constructor(public router: Router) {
    const selectedLanguage = localStorage.getItem('selectedLanguage') || 'en';
    // translateService.setDefaultLang(selectedLanguage);
    // translateService.use(selectedLanguage);
   }

  checkUrlContainsRouteOutOfLayout(): boolean {
    for (const value of this.routesOutOfLayout) {
      if (this.router.url.includes(value)) {
        return false;
      }
    }
    return true;
  }
}
