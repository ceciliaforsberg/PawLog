using System;
using System.Collections.Generic;
using System.Text;

namespace PawLog.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Breed { get; set; }
        public DateOnly? DateOfBirth { get; set; }

        public List<TrainingSession> TrainingSessions { get; set; } = new();
    }
}
