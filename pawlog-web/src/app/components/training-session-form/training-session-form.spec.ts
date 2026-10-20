import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrainingSessionForm } from './training-session-form';

describe('TrainingSessionForm', () => {
  let component: TrainingSessionForm;
  let fixture: ComponentFixture<TrainingSessionForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainingSessionForm],
    }).compileComponents();

    fixture = TestBed.createComponent(TrainingSessionForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
