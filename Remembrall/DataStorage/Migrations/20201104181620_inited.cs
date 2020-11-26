using Microsoft.EntityFrameworkCore.Migrations;

namespace DataStorage.Migrations
{
    public partial class inited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsDone = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    Relation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "SpecialDates",
                columns: table => new
                {
                    SpecialDateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDates", x => x.SpecialDateId);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    Mail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailId);
                    table.ForeignKey(
                        name: "FK_Emails_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    PhoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.PhoneId);
                    table.ForeignKey(
                        name: "FK_Phones_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SpecialDates",
                columns: new[] { "SpecialDateId", "Day", "Description", "Month" },
                values: new object[,]
                {
                    { 1, 1, "Новогодние выходные", 1 },
                    { 16, 1, "День знаний", 9 },
                    { 15, 12, "День независимости", 6 },
                    { 14, 9, "День Победы", 5 },
                    { 13, 3, "Мир, труд, май!!!", 5 },
                    { 12, 1, "Мир, труд, май!!!", 5 },
                    { 11, 8, "праздник женщин", 3 },
                    { 10, 23, "День защитника отечества", 2 },
                    { 9, 14, "День всех влюбленных", 2 },
                    { 8, 8, "Новогодние выходные", 1 },
                    { 7, 7, "Новогодние выходные", 1 },
                    { 6, 6, "Новогодние выходные", 1 },
                    { 5, 5, "Новогодние выходные", 1 },
                    { 4, 4, "Новогодние выходные", 1 },
                    { 3, 3, "Новогодние выходные", 1 },
                    { 2, 2, "Новогодние выходные", 1 },
                    { 17, 4, "День народного единства", 11 },
                    { 18, 31, "Новый год! С праздником!!!", 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emails_PersonId",
                table: "Emails",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PersonId",
                table: "Phones",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "SpecialDates");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
