using System;
using System.Collections.Generic;
using System.Text;

namespace PawLog.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Repetitions { get; set; }
        public string? Reward { get; set; }
        public int Rating { get; set; }
        public string? Notes { get; set; }

        public int DogId { get; set; }
        public Dog Dog { get; set; } = null!;

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;
    }
}
