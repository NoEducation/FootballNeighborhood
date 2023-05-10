import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

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

  constructor(public router: Router,
    private readonly translateService: TranslateService) {
    const selectedLanguage = localStorage.getItem('selectedLanguage') || 'pl';
    translateService.setDefaultLang(selectedLanguage);
    translateService.use(selectedLanguage);
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
