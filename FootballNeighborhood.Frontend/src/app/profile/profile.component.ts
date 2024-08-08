import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { CurrentUserService } from '../services/current-user.service';
import { UsersService } from '../services/users.service';
import { GetCurrentUserReponse } from './model/get-current-user-response.model';
import { FormBuilder, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { ComponentViewModeEnum } from '../models/common/component-view-mode.enum';
import { UpdateCurrentUserRequest } from './model/update-current-user-request.model';
import { NotificationType } from '../models/common/notification-type.constraint';
import { NotificationService } from '../services/communication/notification.service';
import { RolesEnum } from '../models/common/roles.enum';
import { IdAndName } from '../models/common/id-and-name.model';
import * as moment from 'moment';
import { GenderEnum } from '../models/user/gender.enum';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  
  viewMode : ComponentViewModeEnum = ComponentViewModeEnum.View;
  profileForm: FormGroup;
  
  readonly viewModeValues = ComponentViewModeEnum;
  readonly minDate = new Date(1900, 1, 1);
  readonly maxDate = new Date();

  readonly userRoles: IdAndName[] = [
    { 
      id : RolesEnum.Player,
      name : 'Uczestnik',
    },
    { 
      id : RolesEnum.MatchOrganizer,
      name : 'Ogranizator' ,
    },
    { 
      id : RolesEnum.Admin,
      name : 'Administrator',
    },
  ];

  readonly genders: IdAndName[] = [
    { 
      id : GenderEnum.Men,
      name : 'Męzczyzna',
    },
    { 
      id : GenderEnum.Woman,
      name : 'Kobieta' ,
    },
  ];

  private userId: number;

  constructor(private readonly location: Location,
    private readonly currentUserService: CurrentUserService,
    private readonly userService: UsersService,
    private readonly fb: FormBuilder,
    private readonly notificationService: NotificationService
  ) { }

  ngOnInit() {
  
    this.initProfileForm();
    this.userId = this.currentUserService.getCurrentUserId();
    this.loadUserProfile();
  }

  back() : void{
    this.location.back()
  }

  edit() : void{
    this.viewMode = this.viewModeValues.Edit;
    this.profileForm.enable();
    this.profileForm.controls['role'].disable();
  }

  discardChanges() : void{
    this.viewMode = this.viewModeValues.View;
    this.loadUserProfile();
  } 

  saveChanges() : void {

    this.profileForm.markAllAsTouched();

    if(this.profileForm.invalid) return;

    let request : UpdateCurrentUserRequest = {
      userId: this.userId,
      name: this.profileForm.controls['name'].value,
      surname: this.profileForm.controls['surname'].value,
      email: this.profileForm.controls['email'].value,
      phone: this.profileForm.controls['phone'].value,
      birthDate: this.profileForm.controls['birthDate'].value,
      gender: this.profileForm.controls['gender'].value,
      description: this.profileForm.controls['description'].value,
    }

    this.userService.updateCurrentUser(request).subscribe({
      next: (response) => {
        this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
        this.viewMode = this.viewModeValues.View;
        this.loadUserProfile();
      }
    });
  }

  private initProfileForm() : void{
    this.profileForm = this.fb.group({
      surname: ['', ],
      name: ['', ],
      email: ['', [Validators.required, Validators.email]],
      role: ['', ],
      phone: ['',  [Validators.pattern('^[- +()0-9]+$')]],
      birthDate: [''],
      gender: ['', ],
      description: [''],
    });
  }

  private loadUserProfile() {
    this.userService.getCurrentUser(this.userId).subscribe({
      next: (response) => {
        this.setValuesProfileForm(response.result);
        this.profileForm.disable();
      }
    });
  }

  private setValuesProfileForm(userData : GetCurrentUserReponse) : void{
    this.profileForm.controls['surname'].setValue(userData.surname);
    this.profileForm.controls['name'].setValue(userData.name);
    this.profileForm.controls['email'].setValue(userData.email);
    this.profileForm.controls['role'].setValue(userData.role);
    this.profileForm.controls['phone'].setValue(userData.phone);
    this.profileForm.controls['birthDate'].setValue(userData.birthDate);
    this.profileForm.controls['gender'].setValue(userData.gender);
    this.profileForm.controls['description'].setValue(userData.description);
  }
}
