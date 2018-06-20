using Microsoft.EntityFrameworkCore.Migrations;

namespace TreeReorder.Migrations
{
    public partial class LongSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "Nodes",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Nodes",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
