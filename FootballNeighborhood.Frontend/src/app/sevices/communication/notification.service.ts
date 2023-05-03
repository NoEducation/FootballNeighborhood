import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomNotificationComponent } from 'src/app/common/custom-notification/custom-notification.component';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {

    constructor(private snackBar: MatSnackBar) { }

    displayNotification(message: string, icon: string) {
        this.snackBar.openFromComponent(CustomNotificationComponent, {
            data: {message: message, icon: icon},
            duration: 5000,
            verticalPosition: 'top',
            horizontalPosition: 'right',
            panelClass: [`${icon}-notification-style`]
        });
    }
}
