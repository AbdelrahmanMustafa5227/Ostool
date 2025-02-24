using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Ostool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changingPKtoCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchList",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_WatchList_VisitorId",
                table: "WatchList");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WatchList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchList",
                table: "WatchList",
                columns: new[] { "VisitorId", "CarId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchList",
                table: "WatchList");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "WatchList",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchList",
                table: "WatchList",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_VisitorId",
                table: "WatchList",
                column: "VisitorId");
        }
    }
}