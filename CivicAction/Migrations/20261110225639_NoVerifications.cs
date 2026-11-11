using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivicAction.Migrations
{
    /// <inheritdoc />
    public partial class NoVerifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Account_StudentId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Update_Verification_VerificationId",
                table: "Update");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_Update_VerificationId",
                table: "Update");

            migrationBuilder.DropIndex(
                name: "IX_Project_StudentId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "VerificationId",
                table: "Update");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "Hours",
                table: "Project",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "StudentIds",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Project_ProjectId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ProjectId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "StudentIds",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "VerificationId",
                table: "Update",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Hours",
                table: "Project",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Project",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdminId = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: true),
                    StudentId = table.Column<string>(type: "TEXT", nullable: true),
                    Feedback = table.Column<string>(type: "TEXT", nullable: true),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verification_Account_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verification_Account_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verification_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Update_VerificationId",
                table: "Update",
                column: "VerificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_StudentId",
                table: "Project",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_AdminId",
                table: "Verification",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_ProjectId",
                table: "Verification",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_StudentId",
                table: "Verification",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Account_StudentId",
                table: "Project",
                column: "StudentId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Verification_VerificationId",
                table: "Update",
                column: "VerificationId",
                principalTable: "Verification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
