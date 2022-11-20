using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Access_Level.Migrations
{
    public partial class AddedReviewQueueToCollectionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "review_queue",
                table: "collections",
                type: "integer",
                nullable: false,
                defaultValueSql: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "review_queue",
                table: "collections");
        }
    }
}
