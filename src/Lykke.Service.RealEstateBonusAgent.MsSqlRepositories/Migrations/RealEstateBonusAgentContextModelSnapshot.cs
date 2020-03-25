﻿// <auto-generated />
using System;
using Lykke.Service.RealEstateBonusAgent.MsSqlRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Migrations
{
    [DbContext(typeof(RealEstateBonusAgentContext))]
    partial class RealEstateBonusAgentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("realestatebonusagent")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Entities.DownPaymentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<decimal>("CalculatedCommissionAmount")
                        .HasColumnName("calculated_commission_amount");

                    b.Property<string>("CurrencyCode")
                        .HasColumnName("currency_code");

                    b.Property<string>("CustomerId")
                        .HasColumnName("customer_id");

                    b.Property<decimal>("DownPaymentPercentage")
                        .HasColumnName("down_payment_percentage");

                    b.Property<decimal>("FirstInstallmentAmount")
                        .HasColumnName("first_installment_amount");

                    b.Property<bool>("IsFullReward")
                        .HasColumnName("is_full_reward");

                    b.Property<decimal>("NetPropertyPrice")
                        .HasColumnName("net_property_price");

                    b.Property<string>("ReferralLeadId")
                        .HasColumnName("referral_lead_id");

                    b.Property<Guid>("ReferrerId")
                        .HasColumnName("referrer_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("timestamp");

                    b.HasKey("Id");

                    b.ToTable("down_payment");
                });

            modelBuilder.Entity("Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Entities.InstallmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<decimal>("CompletionAmount")
                        .HasColumnName("completion_amount");

                    b.Property<string>("CurrencyCode")
                        .HasColumnName("currency_code");

                    b.Property<string>("CustomerId")
                        .HasColumnName("customer_id");

                    b.Property<int>("InstallmentNumber")
                        .HasColumnName("installment_number");

                    b.Property<decimal>("NetPropertyPrice")
                        .HasColumnName("net_property_price");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnName("paid_amount");

                    b.Property<string>("ReferralLeadId")
                        .HasColumnName("referral_lead_id");

                    b.Property<string>("ReferrerId")
                        .HasColumnName("referrer_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("timestamp");

                    b.Property<int>("TotalInstallments")
                        .HasColumnName("total_installments");

                    b.HasKey("Id");

                    b.ToTable("deposit");
                });
#pragma warning restore 612, 618
        }
    }
}
