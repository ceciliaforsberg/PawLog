import { Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Dog } from '../models/dog';

@Injectable({providedIn: 'root'})
export class DogApi {
    private http = inject(HttpClient);
    private baseUrl = 'https://localhost:7027/api/dogs';

    getAll(): Observable<Dog[]> {
        return this.http.get<Dog[]>(this.baseUrl);
    }
}
