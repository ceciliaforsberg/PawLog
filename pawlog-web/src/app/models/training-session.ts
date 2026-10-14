export interface TrainingSession {
    id: number;
    date: string;
    repetitions: number;
    reward: string | null;
    rating: number;
    notes: string | null;
    dogId: number;
    dogName: string;
    exerciseId: number;
    exerciseName: string;
}
