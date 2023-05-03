import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-sign-up-dialog',
  templateUrl: './sign-up-dialog.component.html',
  styleUrls: ['../login.component.css']
})
export class SignUpDialogComponent implements OnInit {
  signUpForm: FormGroup;
  dataLoading = false;
  newUser: User;

  constructor(
    private formBuilder: FormBuilder,
    public signUpDialogReference: MatDialogRef<SignUpDialogComponent>,
    private userService: UserService
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
        this.newUser = {
          Username: this.signUpForm.get('username').value,
          Password: this.signUpForm.get('password').value,
          EmailAddress: this.signUpForm.get('emailAddress').value
        };
        this.userService.createAccount(this.newUser).subscribe({
          next: () => {
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
