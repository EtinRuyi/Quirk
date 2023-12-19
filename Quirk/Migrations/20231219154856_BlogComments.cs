using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quirk.Migrations
{
    /// <inheritdoc />
    public partial class BlogComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogPostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df290ba0-1722-373c-8f1c-d687e5764168",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0d3dc26-fd20-4f2c-916a-705a718b7674", "AQAAAAIAAYagAAAAEEPL43wROsERHLMUcZeBQyy49lw9KMco6oz0qGZBy2BdV+9vGx1rIiYQHrw9SLFXPQ==", "54780023-aa8b-4767-a2b7-fc9772201a52" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df290ba0-1722-373c-8f1c-d687e5764168",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c49e99f-79e0-4b2b-9e5a-a2c7f9595365", "AQAAAAIAAYagAAAAEB0Xb1mXD3fnkijmu1xMOUnXdhn9MmJQDAfqS16RO9GgM0g4e81eDsChZOS8KVwXnw==", "2c1a010c-f3b6-45fb-9f45-f03825de6789" });
        }
    }
}
