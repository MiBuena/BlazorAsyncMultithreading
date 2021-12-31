using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopulationCensus.Data.Migrations
{
    public partial class CheckForAddCensusAreaData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreaData_Ages_AgeId",
                table: "CensusAreaData");

            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreaData_Areas_AreaId",
                table: "CensusAreaData");

            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreaData_Ethnicity_EthnicityId",
                table: "CensusAreaData");

            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreaData_Gender_GenderId",
                table: "CensusAreaData");

            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreaData_Year_YearId",
                table: "CensusAreaData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CensusAreaData",
                table: "CensusAreaData");

            migrationBuilder.RenameTable(
                name: "CensusAreaData",
                newName: "CensusAreasData");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreaData_YearId",
                table: "CensusAreasData",
                newName: "IX_CensusAreasData_YearId");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreaData_GenderId",
                table: "CensusAreasData",
                newName: "IX_CensusAreasData_GenderId");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreaData_EthnicityId",
                table: "CensusAreasData",
                newName: "IX_CensusAreasData_EthnicityId");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreaData_AreaId",
                table: "CensusAreasData",
                newName: "IX_CensusAreasData_AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreaData_AgeId",
                table: "CensusAreasData",
                newName: "IX_CensusAreasData_AgeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CensusAreasData",
                table: "CensusAreasData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreasData_Ages_AgeId",
                table: "CensusAreasData",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreasData_Areas_AreaId",
                table: "CensusAreasData",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreasData_Ethnicity_EthnicityId",
                table: "CensusAreasData",
                column: "EthnicityId",
                principalTable: "Ethnicity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreasData_Gender_GenderId",
                table: "CensusAreasData",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreasData_Year_YearId",
                table: "CensusAreasData",
                column: "YearId",
                principalTable: "Year",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreasData_Ages_AgeId",
                table: "CensusAreasData");

            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreasData_Areas_AreaId",
                table: "CensusAreasData");

            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreasData_Ethnicity_EthnicityId",
                table: "CensusAreasData");

            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreasData_Gender_GenderId",
                table: "CensusAreasData");

            migrationBuilder.DropForeignKey(
                name: "FK_CensusAreasData_Year_YearId",
                table: "CensusAreasData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CensusAreasData",
                table: "CensusAreasData");

            migrationBuilder.RenameTable(
                name: "CensusAreasData",
                newName: "CensusAreaData");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreasData_YearId",
                table: "CensusAreaData",
                newName: "IX_CensusAreaData_YearId");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreasData_GenderId",
                table: "CensusAreaData",
                newName: "IX_CensusAreaData_GenderId");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreasData_EthnicityId",
                table: "CensusAreaData",
                newName: "IX_CensusAreaData_EthnicityId");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreasData_AreaId",
                table: "CensusAreaData",
                newName: "IX_CensusAreaData_AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_CensusAreasData_AgeId",
                table: "CensusAreaData",
                newName: "IX_CensusAreaData_AgeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CensusAreaData",
                table: "CensusAreaData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreaData_Ages_AgeId",
                table: "CensusAreaData",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreaData_Areas_AreaId",
                table: "CensusAreaData",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreaData_Ethnicity_EthnicityId",
                table: "CensusAreaData",
                column: "EthnicityId",
                principalTable: "Ethnicity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreaData_Gender_GenderId",
                table: "CensusAreaData",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CensusAreaData_Year_YearId",
                table: "CensusAreaData",
                column: "YearId",
                principalTable: "Year",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
