import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrainingSessionsPage } from './training-sessions-page';

describe('TrainingSessionsPage', () => {
  let component: TrainingSessionsPage;
  let fixture: ComponentFixture<TrainingSessionsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainingSessionsPage],
    }).compileComponents();

    fixture = TestBed.createComponent(TrainingSessionsPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
