using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretSanta.API.Migrations
{
    /// <inheritdoc />
    public partial class SecretSanta0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailTemplatePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimumGiftValue = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false, defaultValue: 1000m),
                    MaximumGiftValue = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false, defaultValue: 2500m),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventParticipant_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignedBuddy",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    AssignedParticipantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedBuddy", x => new { x.ParticipantId, x.AssignedParticipantId });
                    table.ForeignKey(
                        name: "FK_AssignedBuddy_EventParticipant_AssignedParticipantId",
                        column: x => x.AssignedParticipantId,
                        principalTable: "EventParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedBuddy_EventParticipant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "EventParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedBuddy_AssignedParticipantId",
                table: "AssignedBuddy",
                column: "AssignedParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipant_EventId",
                table: "EventParticipant",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedBuddy");

            migrationBuilder.DropTable(
                name: "EventParticipant");

            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
