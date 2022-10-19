using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.PersonService.Data.Migrations
{
    public partial class MigInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    UUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UUID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.id);
                    table.ForeignKey(
                        name: "FK_ContactInfos_Persons_UUID",
                        column: x => x.UUID,
                        principalTable: "Persons",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "UUID", "Company", "Name", "SurName" },
                values: new object[] { 1, "xyz", "Abraham", "Lincoln" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "UUID", "Company", "Name", "SurName" },
                values: new object[] { 2, "xyz", "Ipek", "Ipek" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "UUID", "Company", "Name", "SurName" },
                values: new object[] { 3, "xyz", "Isaac", "Newton" });

            migrationBuilder.InsertData(
                table: "ContactInfos",
                columns: new[] { "id", "Email", "Location", "PhoneNumber", "UUID" },
                values: new object[] { 1, "xyz@any.com", "istanbul", "05554443231", 1 });

            migrationBuilder.InsertData(
                table: "ContactInfos",
                columns: new[] { "id", "Email", "Location", "PhoneNumber", "UUID" },
                values: new object[] { 2, "xyz@any.com", "Ankara", "05554443211", 2 });

            migrationBuilder.InsertData(
                table: "ContactInfos",
                columns: new[] { "id", "Email", "Location", "PhoneNumber", "UUID" },
                values: new object[] { 3, "xyz@any.com", "Ankara", "05554443231", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_UUID",
                table: "ContactInfos",
                column: "UUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
