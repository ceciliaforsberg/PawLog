import { Component } from '@angular/core';
import { TrainingSessionForm } from '../../components/training-session-form/training-session-form';
import { TrainingSessionList } from '../../components/training-session-list/training-session-list';

@Component({
  imports: [TrainingSessionForm, TrainingSessionList],
  selector: 'app-training-sessions-page',
  styleUrl: './training-sessions-page.scss',
  templateUrl: './training-sessions-page.html',
})
export class TrainingSessionsPage {}
