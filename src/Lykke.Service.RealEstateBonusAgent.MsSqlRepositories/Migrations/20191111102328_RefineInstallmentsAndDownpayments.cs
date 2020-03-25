using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Migrations
{
    public partial class RefineInstallmentsAndDownpayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount_amount",
                schema: "realestatebonusagent",
                table: "down_payment");

            migrationBuilder.DropColumn(
                name: "selling_property_price",
                schema: "realestatebonusagent",
                table: "down_payment");

            migrationBuilder.DropColumn(
                name: "vat_amount",
                schema: "realestatebonusagent",
                table: "down_payment");

            migrationBuilder.DropColumn(
                name: "discount_amount",
                schema: "realestatebonusagent",
                table: "deposit");

            migrationBuilder.DropColumn(
                name: "vat_amount",
                schema: "realestatebonusagent",
                table: "deposit");

            migrationBuilder.AlterColumn<string>(
                name: "referral_lead_id",
                schema: "realestatebonusagent",
                table: "down_payment",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<decimal>(
                name: "net_property_price",
                schema: "realestatebonusagent",
                table: "down_payment",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "net_property_price",
                schema: "realestatebonusagent",
                table: "deposit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "completion_amount",
                schema: "realestatebonusagent",
                table: "deposit",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completion_amount",
                schema: "realestatebonusagent",
                table: "deposit");

            migrationBuilder.AlterColumn<Guid>(
                name: "referral_lead_id",
                schema: "realestatebonusagent",
                table: "down_payment",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "net_property_price",
                schema: "realestatebonusagent",
                table: "down_payment",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "discount_amount",
                schema: "realestatebonusagent",
                table: "down_payment",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "selling_property_price",
                schema: "realestatebonusagent",
                table: "down_payment",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "vat_amount",
                schema: "realestatebonusagent",
                table: "down_payment",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "net_property_price",
                schema: "realestatebonusagent",
                table: "deposit",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "discount_amount",
                schema: "realestatebonusagent",
                table: "deposit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "vat_amount",
                schema: "realestatebonusagent",
                table: "deposit",
                nullable: true);
        }
    }
}
