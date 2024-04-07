﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaSync.Migrations
{
    /// <inheritdoc />
    public partial class RemovePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "MegaLinks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "MegaLinks",
                type: "TEXT",
                nullable: true);
        }
    }
}
