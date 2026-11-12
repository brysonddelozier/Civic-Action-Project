using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivicAction.Migrations
{
    /// <inheritdoc />
    public partial class Inclusions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Project_ProjectId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Update_Account_StudentId",
                table: "Update");

            migrationBuilder.DropForeignKey(
                name: "FK_Update_Project_ProjectId",
                table: "Update");

            migrationBuilder.DropIndex(
                name: "IX_Account_ProjectId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StudentIds",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "HoursDone",
                table: "Update",
                newName: "Hours");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Update",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Update",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Inclusion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Hours = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inclusion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inclusion_Account_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inclusion_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inclusion_ProjectId",
                table: "Inclusion",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Inclusion_StudentId",
                table: "Inclusion",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Account_StudentId",
                table: "Update",
                column: "StudentId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Project_ProjectId",
                table: "Update",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Update_Account_StudentId",
                table: "Update");

            migrationBuilder.DropForeignKey(
                name: "FK_Update_Project_ProjectId",
                table: "Update");

            migrationBuilder.DropTable(
                name: "Inclusion");

            migrationBuilder.RenameColumn(
                name: "Hours",
                table: "Update",
                newName: "HoursDone");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Update",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Update",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Hours",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentIds",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Account",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_ProjectId",
                table: "Account",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Project_ProjectId",
                table: "Account",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Account_StudentId",
                table: "Update",
                column: "StudentId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Project_ProjectId",
                table: "Update",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
