﻿// <auto-generated />
using System;
using Chameleon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chameleon.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240429185229_RemoveUniqueRoleName")]
    partial class RemoveUniqueRoleName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Chameleon.Application.Common.DataAccess.Entities.ContactDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<Guid>("LocalityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_ContactDetails");

                    b.HasIndex("CountryId");

                    b.HasIndex("LocalityId");

                    b.ToTable("ContactDetails", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.Absent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<Guid>("MadeById")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_company");

                    b.HasIndex("MadeById")
                        .IsUnique();

                    b.ToTable("Absent", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AbsentDetailsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CompanyIGuid")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("IsEnd")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsMadeIt")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("MemoryDetailsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TaskOrEventDetailsId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_company");

                    b.HasIndex("AbsentDetailsId")
                        .IsUnique();

                    b.HasIndex("MemoryDetailsId")
                        .IsUnique();

                    b.HasIndex("TaskOrEventDetailsId")
                        .IsUnique();

                    b.ToTable("Card", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BusinessNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ContactDetailsId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_company");

                    b.HasIndex("ContactDetailsId");

                    b.HasIndex("TutorId");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.CompanyCard", b =>
                {
                    b.Property<Guid>("CompanyGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CardGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CardId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.HasKey("CompanyGuid", "CardGuid");

                    b.HasIndex("CardId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyCards");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.CompanyUser", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("CompanyId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyUsers");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.Memory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("MadeById")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_company");

                    b.HasIndex("MadeById")
                        .IsUnique();

                    b.ToTable("Memory", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.TaskOrEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("MadeById")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_company");

                    b.HasIndex("MadeById")
                        .IsUnique();

                    b.ToTable("TaskOrEvent", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.TaskOrEventUser", b =>
                {
                    b.Property<Guid>("TaskOrEventGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TaskOrEvenId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("TaskOrEventGuid", "UserGuid");

                    b.HasIndex("TaskOrEvenId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskOrEventUsers");
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_Country");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.Locality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_Locality");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Locality", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("longtext")
                        .HasDefaultValue("CUSTOMER");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.HasKey("Id")
                        .HasName("PK_Roles");

                    b.HasIndex("CompanyId");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BursDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasAnnotation("RegularExpression", "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Z|a-z]{2,6}$");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasAnnotation("RegularExpression", "^(\\\\+|00)\\\\d{1,4}[\\\\s/0-9]*$");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlComputedColumn(b.Property<DateTime>("UpdatedAt"));

                    b.Property<Guid>("ValidationCode")
                        .HasColumnType("char(36)");

                    b.HasKey("Id")
                        .HasName("PK_user");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.UsersContactDetails", b =>
                {
                    b.Property<Guid>("ContactDetailsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("ContactDetailsId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersContactDetails");
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.UsersRoles", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("Chameleon.Application.Common.DataAccess.Entities.ContactDetails", b =>
                {
                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.Country", "Country")
                        .WithMany("ContactDetailsList")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.Locality", "Locality")
                        .WithMany("ContactDetailsList")
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Locality");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.Absent", b =>
                {
                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.User", "MadeBy")
                        .WithOne()
                        .HasForeignKey("Chameleon.Application.CompanySetting.DataAccess.Entities.Absent", "MadeById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MadeBy");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.Card", b =>
                {
                    b.HasOne("Chameleon.Application.CompanySetting.DataAccess.Entities.Absent", "AbsentDetails")
                        .WithOne()
                        .HasForeignKey("Chameleon.Application.CompanySetting.DataAccess.Entities.Card", "AbsentDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chameleon.Application.CompanySetting.DataAccess.Entities.Memory", "MemoryDetails")
                        .WithOne()
                        .HasForeignKey("Chameleon.Application.CompanySetting.DataAccess.Entities.Card", "MemoryDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chameleon.Application.CompanySetting.DataAccess.Entities.TaskOrEvent", "TaskOrEventDetails")
                        .WithOne()
                        .HasForeignKey("Chameleon.Application.CompanySetting.DataAccess.Entities.Card", "TaskOrEventDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AbsentDetails");

                    b.Navigation("MemoryDetails");

                    b.Navigation("TaskOrEventDetails");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.Company", b =>
                {
                    b.HasOne("Chameleon.Application.Common.DataAccess.Entities.ContactDetails", null)
                        .WithMany("Companies")
                        .HasForeignKey("ContactDetailsId");

                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.User", "Tutor")
                        .WithMany()
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.CompanyCard", b =>
                {
                    b.HasOne("Chameleon.Application.CompanySetting.DataAccess.Entities.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chameleon.Application.CompanySetting.DataAccess.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.CompanyUser", b =>
                {
                    b.HasOne("Chameleon.Application.CompanySetting.DataAccess.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.Memory", b =>
                {
                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.User", "MadeBy")
                        .WithOne()
                        .HasForeignKey("Chameleon.Application.CompanySetting.DataAccess.Entities.Memory", "MadeById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MadeBy");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.TaskOrEvent", b =>
                {
                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.User", "MadeBy")
                        .WithOne()
                        .HasForeignKey("Chameleon.Application.CompanySetting.DataAccess.Entities.TaskOrEvent", "MadeById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MadeBy");
                });

            modelBuilder.Entity("Chameleon.Application.CompanySetting.DataAccess.Entities.TaskOrEventUser", b =>
                {
                    b.HasOne("Chameleon.Application.CompanySetting.DataAccess.Entities.TaskOrEvent", "TaskOrEven")
                        .WithMany()
                        .HasForeignKey("TaskOrEvenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskOrEven");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.Roles", b =>
                {
                    b.HasOne("Chameleon.Application.CompanySetting.DataAccess.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.UsersContactDetails", b =>
                {
                    b.HasOne("Chameleon.Application.Common.DataAccess.Entities.ContactDetails", "ContactDetails")
                        .WithMany()
                        .HasForeignKey("ContactDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactDetails");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.UsersRoles", b =>
                {
                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.Roles", "Roles")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chameleon.Application.HumanSetting.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chameleon.Application.Common.DataAccess.Entities.ContactDetails", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.Country", b =>
                {
                    b.Navigation("ContactDetailsList");
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.Locality", b =>
                {
                    b.Navigation("ContactDetailsList");
                });

            modelBuilder.Entity("Chameleon.Application.HumanSetting.DataAccess.Entities.Roles", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
