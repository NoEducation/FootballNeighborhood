import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LoadingState } from './loadingstate.interface';

@Injectable({
    providedIn: 'root'
})
export class LoadingService {
    private loadingSubject = new Subject<LoadingState>();
    loadingState = this.loadingSubject.asObservable();

    loadingStarted() {
        setTimeout(() => {
            this.loadingSubject.next({ dataLoading: true } as LoadingState);
        }, 0);
    }

    loadingCompleted() {
        setTimeout(() => {
            this.loadingSubject.next({ dataLoading: false } as LoadingState);
        }, 0);
    }
}
