using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmarOvijog.Migrations
{
    public partial class ComplaintwithRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnionId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpazilaId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Complaints",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_DistrictId",
                table: "Complaints",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_DivisionId",
                table: "Complaints",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UnionId",
                table: "Complaints",
                column: "UnionId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UpazilaId",
                table: "Complaints",
                column: "UpazilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserId",
                table: "Complaints",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Districts_DistrictId",
                table: "Complaints",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Divisions_DivisionId",
                table: "Complaints",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Unions_UnionId",
                table: "Complaints",
                column: "UnionId",
                principalTable: "Unions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Upazilas_UpazilaId",
                table: "Complaints",
                column: "UpazilaId",
                principalTable: "Upazilas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Users_UserId",
                table: "Complaints",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Districts_DistrictId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Divisions_DivisionId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Unions_UnionId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Upazilas_UpazilaId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Users_UserId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_DistrictId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_DivisionId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UnionId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UpazilaId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UnionId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UpazilaId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Complaints");
        }
    }
}
