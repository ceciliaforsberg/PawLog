export interface CreateTrainingSessionRequest {
    date: string;
    repetitions: number;
    reward: string | null;
    rating: number;
    notes: string | null;
    dogId: number;
    exerciseId: number;
}
