import { Component, inject  , OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { TrainingSessionApi } from '../../services/training-session-api';
import { TrainingSession } from '../../models/training-session';
import { DogApi } from '../../services/dog-api';
import { Dog } from '../../models/dog';

@Component({
  imports: [DatePipe],
  selector: 'app-training-session-list',
  styleUrl: './training-session-list.scss',
  templateUrl: './training-session-list.html',
})
export class TrainingSessionList implements OnInit {
  private trainingSessionApi = inject(TrainingSessionApi);
  private dogApi = inject(DogApi);

  trainingSessions = signal<TrainingSession[]>([]);
  dogs = signal<Dog[]>([]);
  selectedDogId = signal<number | undefined>(undefined);

  ngOnInit(): void {
    this.dogApi.getAll().subscribe(data => this.dogs.set(data));
    this.load();
  }

  load(): void {
    this.trainingSessionApi.getAll(this.selectedDogId()).subscribe({
      next: data => this.trainingSessions.set(data),
      error: err => console.error('Failed to load training sessions', err),
    }); 
  }

  onDogChange(dogId: string): void {
    this.selectedDogId.set(dogId ? Number(dogId) : undefined);
    this.load();
  }
}
