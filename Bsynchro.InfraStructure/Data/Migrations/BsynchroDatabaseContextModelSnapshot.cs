﻿// <auto-generated />
using System;
using Bsynchro.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bsynchro.InfraStructure.Migrations
{
    [DbContext(typeof(BsynchroDatabaseContext))]
    partial class BsynchroDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bsynchro.Domain.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    b.Property<double?>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AccountId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("Bsynchro.Domain.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<double?>("InitialCredit")
                        .HasColumnType("float");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Bsynchro.Domain.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .HasColumnType("int")
                        .HasColumnName("TransactionID");

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("datetime");

                    b.HasKey("TransactionId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("Bsynchro.Domain.Account", b =>
                {
                    b.HasOne("Bsynchro.Domain.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Account_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Bsynchro.Domain.Transaction", b =>
                {
                    b.HasOne("Bsynchro.Domain.Account", "Recipient")
                        .WithMany("TransactionRecipients")
                        .HasForeignKey("RecipientId")
                        .IsRequired()
                        .HasConstraintName("FK_Transaction_Account1");

                    b.HasOne("Bsynchro.Domain.Account", "Sender")
                        .WithMany("TransactionSenders")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK_Transaction_Account");

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Bsynchro.Domain.Account", b =>
                {
                    b.Navigation("TransactionRecipients");

                    b.Navigation("TransactionSenders");
                });

            modelBuilder.Entity("Bsynchro.Domain.Customer", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
