using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorMovieTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Watched = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Biography", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { 1, "Austrailian actor known for his role as Jake Sully in Avatar.", new DateTime(1976, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sam Worthington" },
                    { 2, "American actress who portrayed Neytiri in Avatar and Gamora in the Marvel Cinematic Universe.", new DateTime(1978, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zoe Saldana" },
                    { 3, "Veteran actress known for the Alien franchise and Avatar.", new DateTime(1949, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sigourney Weaver" },
                    { 4, "Oscar-winning actor best known for Inception, Titanic, and The Revenant.", new DateTime(1974, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leonardo DiCaprio" },
                    { 5, "American actor who starred in Inception, 500 Days of Summer, and Looper.", new DateTime(1981, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joseph Gordon-Levitt" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Genre", "ReleaseYear", "Title", "Watched" },
                values: new object[,]
                {
                    { 1, "A paraplegic marine is sent to the alien world of Pandora on a unique mission but becomes torn between following orders and protecting an indigenous civilization.", "James Cameron", "Sci-Fi", 2000, "Avatar", true },
                    { 2, "Two imprisoned men develop a deep friendship and find hope and redemption while serving long sentences in Shawshank State Penitentiary.", "Frank Darabont", "Drama", 1994, "The Shawshank Redemption", false },
                    { 3, "A skilled thief who steals secrets by infiltrating dreams is offered a chance to erase his criminal record by performing an impossible 'inception'—planting an idea into a target's subconscious.", "Christopher Nolan", "Sci-Fi", 2010, "Inception", true },
                    { 4, "A young girl enters a magical world of spirits and gods where she must find her courage to save her parents and return home.", "Hayao Miyazaki", "Animation", 2001, "Spirited Away", true },
                    { 5, "Set in the turbulent 1960s, a young man travels from Liverpool to New York in search of his father, falling in love and confronting the era's social changes, all set to Beatles music.", "Julie Taymor", "Musical", 2007, "Across the Universe", true },
                    { 6, "The aging patriarch of a crime family transfers control of his empire to his reluctant son, unraveling a saga of power, loyalty, and betrayal.", "Francis Ford Coppola", "Crime", 1972, "The Godfather", false }
                });

            migrationBuilder.InsertData(
                table: "ActorMovie",
                columns: new[] { "ActorsId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 4, 3 },
                    { 5, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesId",
                table: "ActorMovie",
                column: "MoviesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
