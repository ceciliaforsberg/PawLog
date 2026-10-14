import { Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Exercise } from '../models/exercise';

@Injectable({providedIn: 'root'})
export class ExerciseApi {
    private http = inject(HttpClient);
    private baseUrl = 'https://localhost:7027/api/exercises';

    getAll(): Observable<Exercise[]> {
        return this.http.get<Exercise[]>(this.baseUrl);
    }
}
