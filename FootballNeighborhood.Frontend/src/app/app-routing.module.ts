import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MatchesComponent } from './matches/matches.component';
import { AuthenticationGuardService } from './sevices/authentication/authentication-guard.service';
import { MatchDetailsComponent } from './matches/match-details/match-details.component';
import { FindMatchesComponent } from './find-matches/find-matches.component';

const routes: Routes = [
  {
    path: 'matches',
    component: MatchesComponent,
    canActivate: [AuthenticationGuardService]
  },
  {
    path: 'matches/matchDetails/:id',
    component: MatchDetailsComponent,
    canActivate: [AuthenticationGuardService]
  },
  {
    path: 'find-matches',
    component: FindMatchesComponent,
    canActivate: [AuthenticationGuardService]
  },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
