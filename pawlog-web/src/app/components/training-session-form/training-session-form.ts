import { Component, inject, OnInit, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Dog } from '../../models/dog';
import { Exercise } from '../../models/exercise';
import { CreateTrainingSessionRequest } from '../../models/create-training-session-request';
import { TrainingSessionApi } from '../../services/training-session-api';
import { DogApi } from '../../services/dog-api';
import { ExerciseApi } from '../../services/exercise-api';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-training-session-form',
  styleUrl: './training-session-form.scss',
  templateUrl: './training-session-form.html',
})
export class TrainingSessionForm  implements OnInit{
  private fb = inject(FormBuilder);
  private dogApi = inject(DogApi);
  private exerciseApi = inject(ExerciseApi)
  private trainingSessionApi = inject(TrainingSessionApi);

  created = output<void>();

  dogs = signal<Dog[]>([]);
  exercises = signal<Exercise[]>([]);
  errorMessage = signal<string | null>(null);

  form = this.fb.group({
    date: [this.now(), Validators.required],
    dogId: [null as number | null, Validators.required],
    exerciseId: [null as number | null, Validators.required],
    repetitions: [10, [Validators.required, Validators.min(1)]],
    rating: [3, [Validators.required, Validators.min(1), Validators.max(5)]],
    reward: [''],
    notes: [''],
  })

  ngOnInit(): void {
    this.dogApi.getAll().subscribe(data => this.dogs.set(data));
    this.exerciseApi.getAll().subscribe(data => this.exercises.set(data));
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const value = this.form.getRawValue();
    const request: CreateTrainingSessionRequest = {
      date: value.date!,
      dogId: value.dogId!,
      exerciseId: value.exerciseId!,
      repetitions: value.repetitions!,
      rating: value.rating!,
      reward: value.reward || null,
      notes: value.notes || null,
    };

    this.trainingSessionApi.createTrainingSession(request).subscribe({
      next: () => {
        this.errorMessage.set(null);
        this.form.patchValue({ date: this.now(), notes: '' });
        this.created.emit();
      },
      error: (err) => {
        console.error('Failed to create training session', err);
        this.errorMessage.set('Kunde inte skapa träningspass. Försök igen.');
      }
    });
  }

  private now(): string {
    const date = new Date();
    date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
    return date.toISOString().slice(0, 16);
  }
}
