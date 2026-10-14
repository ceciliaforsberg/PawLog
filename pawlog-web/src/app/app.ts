import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TrainingSessionList } from './components/training-session-list/training-session-list';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TrainingSessionList],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('pawlog-web');
}
