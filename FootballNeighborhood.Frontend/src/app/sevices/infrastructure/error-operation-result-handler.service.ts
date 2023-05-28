import { Injectable } from "@angular/core";
import { NotificationType } from "src/app/models/common/notification-type.constraint";
import { OperationResult } from "src/app/models/infrastructure/operation-result.model";
import { NotificationService } from "../communication/notification.service";

@Injectable({
  providedIn: 'root'
})
export class ErrorOperationResultHandlerService{

  constructor(private readonly notificationService: NotificationService)
  {}

  handleError<T>(result : OperationResult<T> | any ) : void{
    if ((result?.status == 400 || result?.status == 401)) return;
    if (!result.Success) this.notificationService.displayNotification(result.error, NotificationType.ERROR);
  }
}
