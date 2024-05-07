import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NotificationType } from '../models/common/notification-type.constraint';
import { NotificationService } from '../sevices/communication/notification.service';
import { UserConfirmationService } from '../sevices/user-confirmation.service';

@Component({
  selector: 'app-confirmation-user',
  templateUrl: './confirmation-user.component.html',
  styleUrls: ['./confirmation-user.component.css']
})
export class ConfirmationUserComponent implements OnInit {

  routeParamsInvalid = false;
  userId: number;
  code: string;
  success = false;
  verificationInProgress = false;
  isConfirmationActive = false;
  year = new Date().getFullYear();

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly notificationService: NotificationService,
    private readonly translateService: TranslateService,
    private readonly userConfirmationService: UserConfirmationService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.userId = params[`userId`];
      this.code = params[`code`];

      this.checkQueryParams();
      this.checkIsActiveConfirmation();
    });
  }

  redirectToLoginPage(): void {
    this.router.navigateByUrl('login');
  }

  resendConfirmation(): void {
    this.userConfirmationService.createConfirmation(this.userId).subscribe({
      next: (response) => {
        this.translateService.get('ConfirmationHasBeenSent').subscribe(message => {
          this.notificationService.displayNotification(message, NotificationType.SUCCESS);
          this.redirectToLoginPage();
        });
      },
      error: (errorResponse) => {
        this.verificationInProgress = false;
      }
    }
    );
  }

  private checkIsActiveConfirmation(): void {
    this.userConfirmationService.isConfirmationActive(this.userId).subscribe({
      next: (response) => {
        this.isConfirmationActive = response.result.IsConfirmationActive;
        if (this.isConfirmationActive) {
          this.confirmUser();
        }
      },
      error: (errorResponse) => {
        this.verificationInProgress = false;
      }
    });
  }

  private confirmUser(): void {
    if (!this.routeParamsInvalid) {
      this.verificationInProgress = true;

      this.userConfirmationService.confirmUser(this.userId, this.code).subscribe({
        next: (response) => {
          this.success = true;
          this.verificationInProgress = false;
        },
        error: (errorResponse) => {
          this.verificationInProgress = false;
          this.router.navigateByUrl('login');
        }
      });
    }
  }

  private checkQueryParams(): void {
    if (this.userId == null) {
      this.routeParamsInvalid = true;
    }

    if (this.code == null) {
      this.routeParamsInvalid = true;
    }

    if (this.routeParamsInvalid) {
      this.notificationService.displayNotification('Błędny link', NotificationType.ERROR);

      this.router.navigateByUrl('login');
    }
  }
}
