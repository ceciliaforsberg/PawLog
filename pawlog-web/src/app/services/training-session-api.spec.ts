import { TestBed } from '@angular/core/testing';
import { TrainingSessionApi } from './training-session-api';

describe('TrainingSessionApi', () => {
  let service: TrainingSessionApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainingSessionApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
