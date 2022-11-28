using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class removedLikeIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Con_ConId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Pro_ProId",
                table: "Likes");

            migrationBuilder.AlterColumn<int>(
                name: "ProId",
                table: "Likes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ConId",
                table: "Likes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Con_ConId",
                table: "Likes",
                column: "ConId",
                principalTable: "Con",
                principalColumn: "ConId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Pro_ProId",
                table: "Likes",
                column: "ProId",
                principalTable: "Pro",
                principalColumn: "ProId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Con_ConId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Pro_ProId",
                table: "Likes");

            migrationBuilder.AlterColumn<int>(
                name: "ProId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Con_ConId",
                table: "Likes",
                column: "ConId",
                principalTable: "Con",
                principalColumn: "ConId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Pro_ProId",
                table: "Likes",
                column: "ProId",
                principalTable: "Pro",
                principalColumn: "ProId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
