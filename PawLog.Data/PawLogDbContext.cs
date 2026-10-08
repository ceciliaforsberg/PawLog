using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PawLog.Models;

namespace PawLog.Data
{
    public class PawLogDbContext : DbContext
    {  
        public PawLogDbContext(DbContextOptions<PawLogDbContext> options) : base(options){}

        public DbSet<Dog> Dogs => Set<Dog>();
        public DbSet<Exercise> Exercises => Set<Exercise>();
        public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainingSession>()
                .HasOne(ts => ts.Exercise)
                .WithMany()
                .HasForeignKey(ts => ts.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
