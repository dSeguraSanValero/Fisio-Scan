using System;
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
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    TreatmentCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.TreatmentId);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "BirthDate", "CreatedBy", "Dni", "FirstSurname", "Name", "SecondSurname" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "724264567", "González", "John", "Rodríguez" },
                    { 2, new DateTime(1995, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "723626246", "Sánchez", "Luis", "Martínez" },
                    { 3, new DateTime(1974, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "745751345", "Sanz", "Rebeca", "Gimenez" },
                    { 4, new DateTime(1965, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "714265724", "Prieto", "Javier", "Alonso" }
                });

            migrationBuilder.InsertData(
                table: "Physios",
                columns: new[] { "PhysioId", "Email", "FirstSurname", "Name", "Password", "RegistrationNumber", "Role", "SecondSurname" },
                values: new object[,]
                {
                    { 1, "admin@admin.com", "Perez", "Juan", "admin", 1568, "Admin", "Martínez" },
                    { 2, "david.calvo@example.com", "Calvo", "David", "1234", 1247, "Physio", "Alonso" },
                    { 3, "rocio.reinosa@example.com", "Reinosa", "Rocío", "1234", 1174, "Physio", "Duate" }
                });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "TreatmentId", "CreatedBy", "PatientId", "TreatmentCause", "TreatmentDate" },
                values: new object[,]
                {
                    { 1, 2, 2, "lumbalgia", new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, "lumbalgia", new DateTime(2024, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 4, "hombro congelado", new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Physios");

            migrationBuilder.DropTable(
                name: "Treatments");
        }
    }
}
