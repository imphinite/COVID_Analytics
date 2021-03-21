using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class InitialDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeGroups",
                columns: table => new
                {
                    AgeGroupID = table.Column<Guid>(nullable: false),
                    LowerBound = table.Column<int>(nullable: false),
                    UpperBound = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeGroups", x => x.AgeGroupID);
                });

            migrationBuilder.CreateTable(
                name: "DimAgeGroups",
                columns: table => new
                {
                    DimAgeGroupKey = table.Column<Guid>(nullable: false),
                    AgeGroupID = table.Column<Guid>(nullable: false),
                    LowerBound = table.Column<int>(nullable: false),
                    UpperBound = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimAgeGroups", x => x.DimAgeGroupKey);
                });

            migrationBuilder.CreateTable(
                name: "DimRegions",
                columns: table => new
                {
                    DimRegionKey = table.Column<Guid>(nullable: false),
                    RegionID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimRegions", x => x.DimRegionKey);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "DimHealthAuthorities",
                columns: table => new
                {
                    DimHealthAuthorityKey = table.Column<Guid>(nullable: false),
                    HealthAuthorityID = table.Column<Guid>(nullable: false),
                    DimRegionKey = table.Column<Guid>(nullable: false),
                    DimRegionKey1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimHealthAuthorities", x => x.DimHealthAuthorityKey);
                    table.ForeignKey(
                        name: "FK_DimHealthAuthorities_DimRegions_DimRegionKey1",
                        column: x => x.DimRegionKey1,
                        principalTable: "DimRegions",
                        principalColumn: "DimRegionKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HealthAuthorities",
                columns: table => new
                {
                    HealthAuthorityID = table.Column<Guid>(nullable: false),
                    RegionID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAuthorities", x => x.HealthAuthorityID);
                    table.ForeignKey(
                        name: "FK_HealthAuthorities_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DimCases",
                columns: table => new
                {
                    DimCaseKey = table.Column<Guid>(nullable: false),
                    CaseID = table.Column<Guid>(nullable: false),
                    ReportedDate = table.Column<DateTime>(type: "date", nullable: false),
                    DimHealthAuthorityKey = table.Column<Guid>(nullable: false),
                    DimHealthAuthorityKey1 = table.Column<Guid>(nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    DimAgeGroupKey = table.Column<Guid>(nullable: false),
                    DimAgeGroupKey1 = table.Column<Guid>(nullable: true),
                    ClassificationReported = table.Column<string>(type: "nvarchar(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimCases", x => x.DimCaseKey);
                    table.ForeignKey(
                        name: "FK_DimCases_DimAgeGroups_DimAgeGroupKey1",
                        column: x => x.DimAgeGroupKey1,
                        principalTable: "DimAgeGroups",
                        principalColumn: "DimAgeGroupKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DimCases_DimHealthAuthorities_DimHealthAuthorityKey1",
                        column: x => x.DimHealthAuthorityKey1,
                        principalTable: "DimHealthAuthorities",
                        principalColumn: "DimHealthAuthorityKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DimHealthServiceDeliveryAreas",
                columns: table => new
                {
                    DimHealthServiceDeliveryAreaKey = table.Column<Guid>(nullable: false),
                    HealthServiceDeliveryAreaID = table.Column<Guid>(nullable: false),
                    DimHealthAuthorityKey1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimHealthServiceDeliveryAreas", x => x.DimHealthServiceDeliveryAreaKey);
                    table.ForeignKey(
                        name: "FK_DimHealthServiceDeliveryAreas_DimHealthAuthorities_DimHealthAuthorityKey1",
                        column: x => x.DimHealthAuthorityKey1,
                        principalTable: "DimHealthAuthorities",
                        principalColumn: "DimHealthAuthorityKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DimLabTestReports",
                columns: table => new
                {
                    DimLabTestReportKey = table.Column<Guid>(nullable: false),
                    LabTestReportID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    DimHealthAuthorityKey = table.Column<Guid>(nullable: false),
                    DimHealthAuthorityKey1 = table.Column<Guid>(nullable: true),
                    NewTests = table.Column<int>(type: "integer default '0'", nullable: false),
                    TotalTests = table.Column<int>(type: "integer default '0'", nullable: false),
                    Positivity = table.Column<decimal>(type: "decimal(10,1) default '0.0'", nullable: false),
                    TurnAround = table.Column<decimal>(type: "decimal(10,1) default '0.0'", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimLabTestReports", x => x.DimLabTestReportKey);
                    table.ForeignKey(
                        name: "FK_DimLabTestReports_DimHealthAuthorities_DimHealthAuthorityKey1",
                        column: x => x.DimHealthAuthorityKey1,
                        principalTable: "DimHealthAuthorities",
                        principalColumn: "DimHealthAuthorityKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseID = table.Column<Guid>(nullable: false),
                    ReportedDate = table.Column<DateTime>(type: "date", nullable: false),
                    HeathAuthorityID = table.Column<Guid>(nullable: false),
                    HealthAuthorityID = table.Column<Guid>(nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    AgeGroupID = table.Column<Guid>(nullable: false),
                    ClassificationReported = table.Column<string>(type: "nvarchar(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_Cases_AgeGroups_AgeGroupID",
                        column: x => x.AgeGroupID,
                        principalTable: "AgeGroups",
                        principalColumn: "AgeGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_HealthAuthorities_HealthAuthorityID",
                        column: x => x.HealthAuthorityID,
                        principalTable: "HealthAuthorities",
                        principalColumn: "HealthAuthorityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HealthServiceDeliveryAreas",
                columns: table => new
                {
                    HealthServiceDeliveryAreaID = table.Column<Guid>(nullable: false),
                    HealthAuthorityID1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthServiceDeliveryAreas", x => x.HealthServiceDeliveryAreaID);
                    table.ForeignKey(
                        name: "FK_HealthServiceDeliveryAreas_HealthAuthorities_HealthAuthorityID1",
                        column: x => x.HealthAuthorityID1,
                        principalTable: "HealthAuthorities",
                        principalColumn: "HealthAuthorityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabTestReports",
                columns: table => new
                {
                    LabTestReportID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    HealthAuthorityID = table.Column<Guid>(nullable: false),
                    NewTests = table.Column<int>(type: "integer default '0'", nullable: false),
                    TotalTests = table.Column<int>(type: "integer default '0'", nullable: false),
                    Positivity = table.Column<decimal>(type: "decimal(10,1) default '0.0'", nullable: false),
                    TurnAround = table.Column<decimal>(type: "decimal(10,1) default '0.0'", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestReports", x => x.LabTestReportID);
                    table.ForeignKey(
                        name: "FK_LabTestReports_HealthAuthorities_HealthAuthorityID",
                        column: x => x.HealthAuthorityID,
                        principalTable: "HealthAuthorities",
                        principalColumn: "HealthAuthorityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_AgeGroupID",
                table: "Cases",
                column: "AgeGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_HealthAuthorityID",
                table: "Cases",
                column: "HealthAuthorityID");

            migrationBuilder.CreateIndex(
                name: "IX_DimCases_DimAgeGroupKey1",
                table: "DimCases",
                column: "DimAgeGroupKey1");

            migrationBuilder.CreateIndex(
                name: "IX_DimCases_DimHealthAuthorityKey1",
                table: "DimCases",
                column: "DimHealthAuthorityKey1");

            migrationBuilder.CreateIndex(
                name: "IX_DimHealthAuthorities_DimRegionKey1",
                table: "DimHealthAuthorities",
                column: "DimRegionKey1");

            migrationBuilder.CreateIndex(
                name: "IX_DimHealthServiceDeliveryAreas_DimHealthAuthorityKey1",
                table: "DimHealthServiceDeliveryAreas",
                column: "DimHealthAuthorityKey1");

            migrationBuilder.CreateIndex(
                name: "IX_DimLabTestReports_DimHealthAuthorityKey1",
                table: "DimLabTestReports",
                column: "DimHealthAuthorityKey1");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthorities_RegionID",
                table: "HealthAuthorities",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_HealthServiceDeliveryAreas_HealthAuthorityID1",
                table: "HealthServiceDeliveryAreas",
                column: "HealthAuthorityID1");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestReports_HealthAuthorityID",
                table: "LabTestReports",
                column: "HealthAuthorityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "DimCases");

            migrationBuilder.DropTable(
                name: "DimHealthServiceDeliveryAreas");

            migrationBuilder.DropTable(
                name: "DimLabTestReports");

            migrationBuilder.DropTable(
                name: "HealthServiceDeliveryAreas");

            migrationBuilder.DropTable(
                name: "LabTestReports");

            migrationBuilder.DropTable(
                name: "AgeGroups");

            migrationBuilder.DropTable(
                name: "DimAgeGroups");

            migrationBuilder.DropTable(
                name: "DimHealthAuthorities");

            migrationBuilder.DropTable(
                name: "HealthAuthorities");

            migrationBuilder.DropTable(
                name: "DimRegions");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
