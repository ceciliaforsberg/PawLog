import { Service } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TrainingSession } from '../models/training-session';
import { CreateTrainingSessionRequest } from '../models/create-training-session-request';

@Injectable({providedIn: 'root'})
export class TrainingSessionApi {
    private http = inject(HttpClient);
    private baseUrl = 'https://localhost:7027/api/trainingsessions';

    getAll(dogId?: number): Observable<TrainingSession[]> {
        let params = new HttpParams();

        if (dogId != null) {
            params = params.set('dogId', dogId);
        }

        return this.http.get<TrainingSession[]>(this.baseUrl, { params });
    }

    createTrainingSession(request: CreateTrainingSessionRequest): Observable<TrainingSession> {
        return this.http.post<TrainingSession>(this.baseUrl, request);
    }
}
