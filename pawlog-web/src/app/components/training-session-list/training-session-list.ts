import { Component, inject  , OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { TrainingSessionApi } from '../../services/training-session-api';
import { TrainingSession } from '../../models/training-session';

@Component({
  imports: [DatePipe],
  selector: 'app-training-session-list',
  styleUrl: './training-session-list.scss',
  templateUrl: './training-session-list.html',
})
export class TrainingSessionList implements OnInit {
  private trainingSessionApi = inject(TrainingSessionApi);
  trainingSessions = signal<TrainingSession[]>([]);

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.trainingSessionApi.getAll().subscribe({
      next: data => this.trainingSessions.set(data),
      error: err => console.error('Failed to load training sessions', err),
    }); 
  }
}
