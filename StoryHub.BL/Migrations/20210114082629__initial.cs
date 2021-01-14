using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryHub.BL.Migrations
{
    public partial class _initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoryCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateOfCreate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storytellers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Folowers = table.Column<int>(nullable: false),
                    QuantityStories = table.Column<int>(nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    About = table.Column<string>(maxLength: 50, nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storytellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateOfCreate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    QuantityLikes = table.Column<int>(nullable: false),
                    QuantityDislikes = table.Column<int>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    StorytellerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_StoryCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "StoryCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stories_Storytellers_StorytellerId",
                        column: x => x.StorytellerId,
                        principalTable: "Storytellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_CategoryId",
                table: "Stories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_StorytellerId",
                table: "Stories",
                column: "StorytellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "StoryCategories");

            migrationBuilder.DropTable(
                name: "Storytellers");
        }
    }
}
