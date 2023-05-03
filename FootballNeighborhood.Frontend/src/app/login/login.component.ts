import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Router } from '@angular/router';
import { SignUpDialogComponent } from './sign-up-dialog/sign-up-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
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
    private dialog: MatDialog,
    private router: Router,
    private employeeService: EmployeeService,
    public utilityService: UtilityService
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
        const username = this.loginForm.get('username').value;
        const password = this.loginForm.get('password').value;
        this.authenticationService.authenticate(username, password).subscribe(
        {
          next:  (result) => {
            sessionStorage.setItem('token', result.Result.Token);
            sessionStorage.setItem('roles', JSON.stringify(result.Result.Roles));
            this.loadLoggedInEmployeeData(result.Result.UserName);
          },
          error : () => {
            this.dataFetched(false);
          }
        });
      }
      catch (error) {
        this.dataFetched(false);
      }
    }
  }

  loadLoggedInEmployeeData(username: string) {
    this.employeeService.getEmployees().subscribe((data) => {
      this.utilityService.transformEmployeeData(data.Result.Employees);
      const loggedInEmployee = data.Result.Employees.find(e => e.Username == username);
      sessionStorage.setItem('loggedInEmployeeDisplayName', loggedInEmployee.DisplayName);
      sessionStorage.setItem('loggedInEmployeeId', loggedInEmployee.EmployeeId.toString());

      this.dataLoading = false;
      this.router.navigateByUrl('/dashboard');
    });
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
      height: '24rem',
      width: '23rem',
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
