using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hitsounder.Game.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "SampleCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Global = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProjectInfoProjectId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleCollections_Projects_ProjectInfoProjectId",
                        column: x => x.ProjectInfoProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "HitSoundSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    DefaultSampleSet = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultSampleType = table.Column<int>(type: "INTEGER", nullable: false),
                    SampleCollectionInfoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HitSoundSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HitSoundSamples_SampleCollections_SampleCollectionInfoId",
                        column: x => x.SampleCollectionInfoId,
                        principalTable: "SampleCollections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HitSoundSamples_SampleCollectionInfoId",
                table: "HitSoundSamples",
                column: "SampleCollectionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SampleCollections_ProjectInfoProjectId",
                table: "SampleCollections",
                column: "ProjectInfoProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HitSoundSamples");

            migrationBuilder.DropTable(
                name: "SampleCollections");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
