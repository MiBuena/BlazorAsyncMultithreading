using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopulationCensus.Data.Migrations
{
    public partial class AddCensusAreaData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CensusAreaData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    AgeId = table.Column<int>(type: "int", nullable: false),
                    EthnicityId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CensusAreaData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CensusAreaData_Ages_AgeId",
                        column: x => x.AgeId,
                        principalTable: "Ages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CensusAreaData_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CensusAreaData_Ethnicity_EthnicityId",
                        column: x => x.EthnicityId,
                        principalTable: "Ethnicity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CensusAreaData_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CensusAreaData_Year_YearId",
                        column: x => x.YearId,
                        principalTable: "Year",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CensusAreaData_AgeId",
                table: "CensusAreaData",
                column: "AgeId");

            migrationBuilder.CreateIndex(
                name: "IX_CensusAreaData_AreaId",
                table: "CensusAreaData",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CensusAreaData_EthnicityId",
                table: "CensusAreaData",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_CensusAreaData_GenderId",
                table: "CensusAreaData",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_CensusAreaData_YearId",
                table: "CensusAreaData",
                column: "YearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CensusAreaData");
        }
    }
}
