using Microsoft.EntityFrameworkCore.Migrations;

namespace TreeReorder.Migrations
{
    public partial class AddRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Nodes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Nodes",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
