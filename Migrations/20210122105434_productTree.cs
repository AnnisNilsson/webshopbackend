using Microsoft.EntityFrameworkCore.Migrations;

namespace webshopbackend.Migrations
{
    public partial class productTree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Price", "Title" },
                values: new object[] { 3, "Ett träd i en solig skogen", "https://images.pexels.com/photos/1563604/pexels-photo-1563604.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500", 650, "Sol Träd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
