import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DogsPage } from './dogs-page';

describe('DogsPage', () => {
  let component: DogsPage;
  let fixture: ComponentFixture<DogsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DogsPage],
    }).compileComponents();

    fixture = TestBed.createComponent(DogsPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
