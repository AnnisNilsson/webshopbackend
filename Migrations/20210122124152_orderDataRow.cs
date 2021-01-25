using Microsoft.EntityFrameworkCore.Migrations;

namespace webshopbackend.Migrations
{
    public partial class orderDataRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderRows",
                columns: new[] { "OrderId", "ProductId" },
                values: new object[] { 2, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderRows",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 2 });
        }
    }
}
