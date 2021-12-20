using Microsoft.EntityFrameworkCore.Migrations;

namespace ParentCareMatts.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetRegistrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GetLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRegistered = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetLogins_GetRegistrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "GetRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GetFamilies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfChildren = table.Column<int>(type: "int", nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetFamilies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetFamilies_GetLogins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "GetLogins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GetFamilies_LoginId",
                table: "GetFamilies",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_GetLogins_RegistrationId",
                table: "GetLogins",
                column: "RegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetFamilies");

            migrationBuilder.DropTable(
                name: "GetLogins");

            migrationBuilder.DropTable(
                name: "GetRegistrations");
        }
    }
}
