// import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { TranslateService } from '@ngx-translate/core';
// import { Observable } from 'rxjs';
// import { tap } from 'rxjs/operators';
// import { NotificationType } from 'src/app/common/constrains/notification-type.constraint';
// import { OperationResultWithGenericError } from 'src/app/models/infrastructure/operation-error-with-generic-error.model';
// import { LoadingService } from './loading.service';
// import { NotificationService } from './notification.service';
// import { environment } from 'src/environments/environment';
// import { OperationResultBase } from 'src/app/models/infrastructure/operation-result-base.model';
// import { ErrorOperationResultHandlerService } from '../error-operation-result-handler.service';
// import { StringExtendsMethod } from 'src/app/common/extends-methods/string-extends-method';


// @Injectable({
//   providedIn: 'root'
// })
// export class CommunicationInterceptorService implements HttpInterceptor {

//   constructor(
//     private notificationService: NotificationService,
//     private translateService: TranslateService,
//     private errorOperationResultHandlerService: ErrorOperationResultHandlerService,
//     private loadingService: LoadingService) { }

//   intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//     this.triggerLoading();

//     let req = request.clone({
//       headers: request.headers.set('Content-Type', 'application/json')
//     });

//     req = this.setAcceptLanguageHeader(req);

//     return next.handle(req).pipe(
//       tap((event: HttpEvent<any>) => {
//         if (event instanceof HttpResponse) {
//           this.finishLoading();
//           if (event.body && event.body.Message) {
//             this.notificationService.displayNotification(event.body.Message, NotificationType.INFO);
//           } else if (event.body?.Code) {
//             this.notificationService.displayNotification(`No Message in response. Code: ${event.body?.Code}. ObjectId: ${event.body.ObjectId}`, NotificationType.INFO);
//           }
//         }
//       },
//         (error: any) => {
//           if (error) {
//             this.finishLoading();
//             try {
//               if ((error?.error?.status == 400 || error?.error?.status == 401)) {
//                 this.handleInvalidRequest(error);
//               }
//               if(error.error?.Errors){
//                 this.handleOperationResult(error.error);
//               }
//               else{
//                 this.handleBasicErrorResult(error.error);
//               }
//             } catch (exception) {
//               this.translateService.get('GenericErrorWithoutErrorCode').subscribe(message => {
//                 this.notificationService.displayNotification(message, NotificationType.ERROR);
//               })
//             }
//           }
//         }
//       ));
//   }

//   private handleInvalidRequest(error: any) : void {
//     const messageKey = environment.production ? 'GenericError' : 'GenericErrorDevelopment';

//     this.translateService.get(messageKey).subscribe(message => {
//       this.notificationService.displayNotification(message, NotificationType.ERROR);
//     });

//     if(!environment.production){
//       console.error(error?.error?.errors);
//     }
//   }

//   private handleBasicErrorResult(error : any) : void {
//     if(error?.Message){
//       this.notificationService.displayNotification(error.Message, NotificationType.ERROR);
//     }
//     else{
//       this.translateService.get('GenericError').subscribe(message => {
//         this.notificationService.displayNotification(message, NotificationType.ERROR);
//       });
//     }
//   }

//   private handleOperationResult(result : OperationResultBase) : void {
//     const resultWithGenericError = result as OperationResultWithGenericError

//     if(resultWithGenericError?.UnexpectedErrorOccurred){
//       this.handleUnexpectedError(resultWithGenericError);
//     }
//     else{
//       this.errorOperationResultHandlerService.handleError(result)
//     }
//   }

//   private handleUnexpectedError(errorDetails: OperationResultWithGenericError) : void {
//       if (!(errorDetails?.DisplayGenericException)) {
//         console.error(errorDetails.Errors[0]?.Error)
//       }

//       const messageKey = !(errorDetails?.DisplayGenericException) ? 'GenericErrorDevelopmentWithErrorCode' : 'GenericErrorWithErrorCode';

//       this.translateService.get('CodeError').subscribe(message => {
//         message = StringExtendsMethod.format(message, errorDetails.Code.toString());
//         console.error(message)
//       });

//       this.translateService.get(messageKey).subscribe(message => {
//         message = StringExtendsMethod.format(message, errorDetails.Code.toString());

//         this.notificationService.displayNotification(message, NotificationType.ERROR);
//       });
//   }

//   private triggerLoading(): void {
//     this.loadingService.loadingStarted();
//   }

//   private finishLoading(): void {
//     this.loadingService.loadingCompleted();
//   }

//   private setAcceptLanguageHeader(request: HttpRequest<any>): HttpRequest<any> {
//     let selectedLanguage = localStorage.getItem('selectedLanguage');

//     if (!selectedLanguage) {
//       selectedLanguage = 'pl';
//     }

//     const req = request.clone({
//       headers: request.headers.set('Accept-Language', selectedLanguage)
//     });

//     return req;
//   }
// }
