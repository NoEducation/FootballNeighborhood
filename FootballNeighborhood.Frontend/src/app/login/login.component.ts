import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Router } from '@angular/router';
import { SignUpDialogComponent } from './sign-up-dialog/sign-up-dialog.component';
import { AuthenticationService } from '../sevices/authentication/authentication.service';
import { NotificationService } from '../sevices/communication/notification.service';
import { UserCredentials } from './models/user-credentials.model';
import { NotificationType } from '../models/common/notification-type.constraint';
import { CurrentUserService } from '../sevices/current-user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  @ViewChild('formDirective') private formDirective: NgForm;
  loginForm: FormGroup;
  forgotPasswordForm: FormGroup;
  dataLoading = false;
  stayLoggedIn = false;
  forgotPassword = false;

  constructor(
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private currentUserService: CurrentUserService,
    private dialog: MatDialog,
    private router: Router,
  ) { }

   ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
    this.forgotPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    })
  }

   logIn(): void {
    if (this.loginForm.valid) {
      try {
        this.dataLoading = true;
        this.loginForm.disable();

        const reqest : UserCredentials = {
          login : this.loginForm.get('username')?.value,
          password: this.loginForm.get('password')?.value
        }

        this.authenticationService.login(reqest).subscribe(
        {
          next: (response) => {
            this.currentUserService.setCurrentUser(response.result.token, response.result.userId);
            this.router.navigateByUrl('/matches');
          },
          error : (errorResponse) => {
            this.notificationService.displayNotification(errorResponse.error.error.error, NotificationType.ERROR);
            this.dataFetched(false);
          }
        });
      }
      catch (error) {
        this.dataFetched(false);
      }
    }
  }

  resetPassword(): void {
    this.notificationService.displayNotification('TODO: Implement Reset Password endpoint', NotificationType.WARNING);
    this.showLoginForm();
    this.forgotPasswordForm.reset();
  }

   dataFetched(reset: boolean = false): void {
    this.dataLoading = false;

    if(reset){
      this.loginForm.reset();
      this.formDirective.resetForm();
    }

    this.loginForm.enable();
  }

  openSignUpDialog(): void {
    this.dialog.open(SignUpDialogComponent, {
      height: '30rem',
      width: '40rem',
    });
  }

  showForgotPasswordForm(): void {
    this.forgotPassword = true;
    this.loginForm.reset();
  }

  showLoginForm(): void {
    this.forgotPassword = false;
    this.forgotPasswordForm.reset();
  }

  toggleLogInSettings(event: MatSlideToggleChange): void {
    this.stayLoggedIn = event.checked;
    this.notificationService.displayNotification(`Stay logged in: ${this.stayLoggedIn}... TODO: Implement \`LogInSettings\` feature`, NotificationType.WARNING);
  }
}
