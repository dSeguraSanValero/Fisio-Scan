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
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    FirstSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Physio"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physios", x => x.PhysioId);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "CreatedBy", "Dni", "FirstSurname", "Name", "SecondSurname" },
                values: new object[,]
                {
                    { 1, 1, "724264567", "González", "John", "Rodríguez" },
                    { 2, 2, "723626246", "Sánchez", "Luis", "Martínez" }
                });

            migrationBuilder.InsertData(
                table: "Physios",
                columns: new[] { "PhysioId", "Email", "FirstSurname", "Name", "Password", "RegistrationNumber", "Role", "SecondSurname" },
                values: new object[,]
                {
                    { 1, "admin@admin.com", "Perez", "Juan", "admin", 1568, "Admin", "Martínez" },
                    { 2, "pakito.perez@example.com", "Calvo", "David", "1234", 1247, "Physio", "Alonso" }
                });
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
