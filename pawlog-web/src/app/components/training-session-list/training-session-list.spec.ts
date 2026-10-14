import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrainingSessionList } from './training-session-list';

describe('TrainingSessionList', () => {
  let component: TrainingSessionList;
  let fixture: ComponentFixture<TrainingSessionList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainingSessionList],
    }).compileComponents();

    fixture = TestBed.createComponent(TrainingSessionList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
