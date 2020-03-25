using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Migrations
{
    public partial class AddDownPaymentAndInstallmentColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "realestatebonusagent");

            migrationBuilder.CreateTable(
                name: "deposit",
                schema: "realestatebonusagent",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    installment_number = table.Column<int>(nullable: false),
                    total_installments = table.Column<int>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    paid_amount = table.Column<decimal>(nullable: false),
                    vat_amount = table.Column<decimal>(nullable: true),
                    net_property_price = table.Column<decimal>(nullable: true),
                    discount_amount = table.Column<decimal>(nullable: true),
                    referral_lead_id = table.Column<string>(nullable: true),
                    referrer_id = table.Column<string>(nullable: true),
                    customer_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "down_payment",
                schema: "realestatebonusagent",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    referrer_id = table.Column<Guid>(nullable: false),
                    referral_lead_id = table.Column<Guid>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    currency_code = table.Column<string>(nullable: true),
                    is_full_reward = table.Column<bool>(nullable: false),
                    down_payment_percentage = table.Column<decimal>(nullable: false),
                    first_installment_amount = table.Column<decimal>(nullable: false),
                    calculated_commission_amount = table.Column<decimal>(nullable: false),
                    vat_amount = table.Column<decimal>(nullable: true),
                    selling_property_price = table.Column<decimal>(nullable: true),
                    net_property_price = table.Column<decimal>(nullable: true),
                    discount_amount = table.Column<decimal>(nullable: true),
                    customer_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_down_payment", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deposit",
                schema: "realestatebonusagent");

            migrationBuilder.DropTable(
                name: "down_payment",
                schema: "realestatebonusagent");
        }
    }
}
