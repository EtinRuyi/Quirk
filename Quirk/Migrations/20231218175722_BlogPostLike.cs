using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quirk.Migrations
{
    /// <inheritdoc />
    public partial class BlogPostLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPostLikes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogPostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
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
                values: new object[] { "9c49e99f-79e0-4b2b-9e5a-a2c7f9595365", "AQAAAAIAAYagAAAAEB0Xb1mXD3fnkijmu1xMOUnXdhn9MmJQDAfqS16RO9GgM0g4e81eDsChZOS8KVwXnw==", "2c1a010c-f3b6-45fb-9f45-f03825de6789" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostLikes_BlogPostId",
                table: "BlogPostLikes",
                column: "BlogPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostLikes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df290ba0-1722-373c-8f1c-d687e5764168",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "009b1e07-9991-48f0-9135-9437f56d940e", "AQAAAAIAAYagAAAAEL1IuOdwBHkqw2goI/wl5eM5nBWn9cf9AY+7zT7Gq/Ad3ty5vnxvQqBPnqeBgEibSg==", "672aa582-b18f-4860-8e88-394495e65004" });
        }
    }
}
