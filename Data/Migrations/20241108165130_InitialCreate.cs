using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FisioScan.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Physios",
                columns: table => new
                {
                    PhysioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physios", x => x.PhysioId);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Dni", "Name" },
                values: new object[] { 1, "730151515", "John Doe" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Dni", "Name" },
                values: new object[] { 2, "730203040", "Pedro Martínez" });

            migrationBuilder.InsertData(
                table: "Physios",
                columns: new[] { "PhysioId", "Email", "LastName", "Name", "Password", "RegistrationNumber" },
                values: new object[] { 1, "pakito.perez@example.com", "Perez", "Pakito", "1234", 1783 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Physios");
        }
    }
}
