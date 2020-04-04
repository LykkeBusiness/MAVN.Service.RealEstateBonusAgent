using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.RealEstateBonusAgent.MsSqlRepositories.Migrations
{
    public partial class AddCurrencyCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "currency_code",
                schema: "realestatebonusagent",
                table: "deposit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency_code",
                schema: "realestatebonusagent",
                table: "deposit");
        }
    }
}
