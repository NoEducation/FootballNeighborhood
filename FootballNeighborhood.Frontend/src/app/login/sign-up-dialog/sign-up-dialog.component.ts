import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthenticationService } from 'src/app/sevices/authentication/authentication.service';
import { RegisterUserRequest } from '../models/register-user-request.model';
import { Roles } from 'src/app/models/common/roles.enum';
import { NotificationService } from 'src/app/sevices/communication/notification.service';
import { NotificationType } from 'src/app/models/common/notification-type.constraint';

@Component({
  selector: 'app-sign-up-dialog',
  templateUrl: './sign-up-dialog.component.html',
  styleUrls: ['../login.component.scss']
})
export class SignUpDialogComponent implements OnInit {
  signUpForm: FormGroup;
  dataLoading = false;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly authenticationService: AuthenticationService,
    private readonly notificationService: NotificationService,
    public signUpDialogReference: MatDialogRef<SignUpDialogComponent>,
  ) { }

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.pattern('(?=.*[0-9])(?=.*[$@$!%*?&]).{11,}')]],
      emailAddress: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit(): void {
    if (this.signUpForm.valid) {
      try {
        this.dataLoading = true;
        this.signUpForm.disable();

        const request : RegisterUserRequest = {
          password : this.signUpForm.get('password')?.value,
          login: this.signUpForm.get('username')?.value,
          email: this.signUpForm.get('emailAddress')?.value,
          role: Roles.Player
        }

        this.authenticationService.register(request).subscribe({
          next: (reponse) => {
            this.notificationService.displayNotification(reponse.result.message, NotificationType.SUCCESS);
            this.dataFetched();
          },
          error: () => {
            this.dataFetched();
          }
        })
      } catch (error) {
        this.dataFetched();
      }
    }
  }

  dataFetched() {
    this.closeSignUpDialog();
    this.dataLoading = false;
    this.signUpForm.enable();
  }

  closeSignUpDialog(): void {
    this.signUpDialogReference.close();
  }
}
