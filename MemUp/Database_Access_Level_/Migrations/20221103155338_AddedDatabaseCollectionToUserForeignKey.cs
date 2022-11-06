using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Access_Level.Migrations
{
    public partial class AddedDatabaseCollectionToUserForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "user_id",
            //    table: "collections",
            //    type: "integer",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "collections_user_id",
            //    table: "collections",
            //    column: "user_id");

            //migrationBuilder.AddForeignKey(
            //    name: "collection_owner",
            //    table: "collections",
            //    column: "user_id",
            //    principalTable: "users",
            //    principalColumn: "user_id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "collection_owner",
            //    table: "collections");

            //migrationBuilder.DropIndex(
            //    name: "collections_user_id",
            //    table: "collections");

            //migrationBuilder.DropColumn(
            //    name: "user_id",
            //    table: "collections");
        }
    }
}
