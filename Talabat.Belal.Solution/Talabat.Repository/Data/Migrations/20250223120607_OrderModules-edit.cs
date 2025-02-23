using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderModulesedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DelivaryDate",
                table: "DeliveryMethods",
                newName: "DeliveryTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "DeliveryMethods",
                newName: "DelivaryDate");
        }
    }
}
