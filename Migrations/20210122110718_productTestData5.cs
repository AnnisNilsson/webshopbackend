using Microsoft.EntityFrameworkCore.Migrations;

namespace webshopbackend.Migrations
{
    public partial class productTestData5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Price", "Title" },
                values: new object[] { 5, "Ett träd rött träd", "https://images.pexels.com/photos/1547813/pexels-photo-1547813.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500", 550, "Röda Träd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
