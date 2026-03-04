using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSimulationEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateManifestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CandidateId = table.Column<int>(type: "INTEGER", nullable: false),
                    ElectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    EconomyPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    EducationPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    WelfarePoints = table.Column<int>(type: "INTEGER", nullable: false),
                    SecurityPoints = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateManifestos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrendEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventName = table.Column<string>(type: "TEXT", nullable: false),
                    TurnNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    EconomyMultiplier = table.Column<double>(type: "REAL", nullable: false),
                    EducationMultiplier = table.Column<double>(type: "REAL", nullable: false),
                    WelfareMultiplier = table.Column<double>(type: "REAL", nullable: false),
                    SecurityMultiplier = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrendEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoterPersonas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    EconomyWeight = table.Column<double>(type: "REAL", nullable: false),
                    EducationWeight = table.Column<double>(type: "REAL", nullable: false),
                    WelfareWeight = table.Column<double>(type: "REAL", nullable: false),
                    SecurityWeight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterPersonas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VoterPersonas",
                columns: new[] { "Id", "Description", "EconomyWeight", "EducationWeight", "Name", "SecurityWeight", "WelfareWeight" },
                values: new object[,]
                {
                    { 1, "「経済」重視。高齢者向け福祉や増税を嫌う。", 1.5, 0.5, "ソウタ（20代・低所得フリーター）", 0.20000000000000001, -1.0 },
                    { 2, "「子育て」最重視。現役世代への負担増を嫌う。", 0.80000000000000004, 2.0, "ミキ（30代・中間層会社員）", 0.5, 0.20000000000000001 },
                    { 3, "「経済」重視。ばらまき福祉を極端に嫌う。", 2.0, 0.5, "ケンジ（40代・富裕層IT経営者）", 0.5, -1.5 },
                    { 4, "「福祉」「治安」重視。変化や年金カットを極端に嫌う。", -0.5, -0.5, "トメ（70代・年金生活者）", 1.5, 2.0 },
                    { 5, "「治安（防災）」「経済（地方創生）」重視。都市部偏重を嫌う。", 1.0, 0.20000000000000001, "マコト（50代・地方農家）", 1.5, 0.80000000000000004 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateManifestos");

            migrationBuilder.DropTable(
                name: "TrendEvents");

            migrationBuilder.DropTable(
                name: "VoterPersonas");
        }
    }
}
