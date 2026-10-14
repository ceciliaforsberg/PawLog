import { Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TrainingSession } from '../models/training-session';

@Injectable({providedIn: 'root'})
export class TrainingSessionApi {
    private http = inject(HttpClient);
    private baseUrl = 'https://localhost:7027/api/trainingsessions';

    getAll(): Observable<TrainingSession[]> {
        return this.http.get<TrainingSession[]>(this.baseUrl);
    }
}
