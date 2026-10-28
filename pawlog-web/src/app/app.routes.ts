import { Routes } from '@angular/router';
import { TrainingSessionsPage } from './pages/training-sessions-page/training-sessions-page';
import { DogsPage } from './pages/dogs-page/dogs-page';
import { ExercisePage } from './pages/exercise-page/exercise-page';

export const routes: Routes = [
   { path: '', redirectTo: 'training-sessions', pathMatch: 'full'},
   { path: 'training-sessions', component: TrainingSessionsPage},
   { path: 'dogs', component: DogsPage},
   {path: 'exercises', component: ExercisePage} 
];
