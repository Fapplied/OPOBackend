using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class UpdatedLikeClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Con_Problem_ProblemId",
                table: "Con");

            migrationBuilder.DropForeignKey(
                name: "FK_Pro_Problem_ProblemId",
                table: "Pro");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemId",
                table: "Pro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProblemId",
                table: "Con",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Con_Problem_ProblemId",
                table: "Con",
                column: "ProblemId",
                principalTable: "Problem",
                principalColumn: "ProblemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pro_Problem_ProblemId",
                table: "Pro",
                column: "ProblemId",
                principalTable: "Problem",
                principalColumn: "ProblemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Con_Problem_ProblemId",
                table: "Con");

            migrationBuilder.DropForeignKey(
                name: "FK_Pro_Problem_ProblemId",
                table: "Pro");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Likes");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemId",
                table: "Pro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemId",
                table: "Con",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Con_Problem_ProblemId",
                table: "Con",
                column: "ProblemId",
                principalTable: "Problem",
                principalColumn: "ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pro_Problem_ProblemId",
                table: "Pro",
                column: "ProblemId",
                principalTable: "Problem",
                principalColumn: "ProblemId");
        }
    }
}
