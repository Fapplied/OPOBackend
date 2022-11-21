using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfilePicture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePicture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_ProfilePicture_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "ProfilePicture",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    ProblemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.ProblemId);
                    table.ForeignKey(
                        name: "FK_Problem_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Con",
                columns: table => new
                {
                    ConId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Con", x => x.ConId);
                    table.ForeignKey(
                        name: "FK_Con_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "ProblemId");
                });

            migrationBuilder.CreateTable(
                name: "Pro",
                columns: table => new
                {
                    ProId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pro", x => x.ProId);
                    table.ForeignKey(
                        name: "FK_Pro_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "ProblemId");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConId = table.Column<int>(type: "int", nullable: true),
                    ProId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Con_ConId",
                        column: x => x.ConId,
                        principalTable: "Con",
                        principalColumn: "ConId");
                    table.ForeignKey(
                        name: "FK_Likes_Pro_ProId",
                        column: x => x.ProId,
                        principalTable: "Pro",
                        principalColumn: "ProId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Con_ProblemId",
                table: "Con",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ConId",
                table: "Likes",
                column: "ConId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ProId",
                table: "Likes",
                column: "ProId");

            migrationBuilder.CreateIndex(
                name: "IX_Pro_ProblemId",
                table: "Pro",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_UserId",
                table: "Problem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfilePictureId",
                table: "User",
                column: "ProfilePictureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Con");

            migrationBuilder.DropTable(
                name: "Pro");

            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ProfilePicture");
        }
    }
}
