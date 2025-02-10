using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ostool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletedSomeCarSpecsRowsForSimplicity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Heigth",
                table: "CarSpecs");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "CarSpecs");

            migrationBuilder.DropColumn(
                name: "Torque",
                table: "CarSpecs");

            migrationBuilder.DropColumn(
                name: "WheelBase",
                table: "CarSpecs");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "CarSpecs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Heigth",
                table: "CarSpecs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "CarSpecs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Torque",
                table: "CarSpecs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "WheelBase",
                table: "CarSpecs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "CarSpecs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}