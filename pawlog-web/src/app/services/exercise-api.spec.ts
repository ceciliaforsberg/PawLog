import { TestBed } from '@angular/core/testing';
import { ExerciseApi } from './exercise-api';

describe('ExerciseApi', () => {
  let service: ExerciseApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExerciseApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
