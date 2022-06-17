using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CPSDevExerciseWeb.Migrations
{
    public partial class CustomerNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerName",
                table: "Orders",
                column: "CustomerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerName",
                table: "Orders");
        }
    }
}
