using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTSPrep_MVC.Migrations
{
    /// <inheritdoc />
    public partial class CurrentChapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Words",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CurrentChapterId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_ProjectId",
                table: "Words",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CurrentChapterId",
                table: "Projects",
                column: "CurrentChapterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Chapters_CurrentChapterId",
                table: "Projects",
                column: "CurrentChapterId",
                principalTable: "Chapters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Projects_ProjectId",
                table: "Words",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Chapters_CurrentChapterId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Projects_ProjectId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_ProjectId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CurrentChapterId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CurrentChapterId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Words",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
