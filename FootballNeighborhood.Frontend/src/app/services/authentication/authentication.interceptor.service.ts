import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable()
export class AuthenticationInterceptorService implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (sessionStorage.getItem('token') != null) {
            const clonedRequest = request.clone({
                headers: request.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('token'))
            });
            return next.handle(clonedRequest).pipe(
                tap(
                    succ => { },
                    error => {
                        if (error.status === 401) {
                            sessionStorage.clear();
                            this.router.navigateByUrl('login');
                            location.reload();
                        }
                    }
                ));
        }
        return next.handle(request.clone());
    }
}
