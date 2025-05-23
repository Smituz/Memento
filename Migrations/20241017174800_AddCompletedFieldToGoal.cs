﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeriDiaryv2.Migrations
{
    /// <inheritdoc />
    public partial class AddCompletedFieldToGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Goals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Goals");
        }
    }
}
