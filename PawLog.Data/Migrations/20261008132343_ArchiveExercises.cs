using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArchiveExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Exercises_ExerciseId",
                table: "TrainingSessions");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Exercises",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Exercises_ExerciseId",
                table: "TrainingSessions",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Exercises_ExerciseId",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Exercises");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Exercises_ExerciseId",
                table: "TrainingSessions",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
