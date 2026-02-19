using LifeOs.Context;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LifeOS.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260219121030_InitialSupabaseSetup")]
    partial class InitialSupabaseSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LifeOs.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorHex")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("XPMultiplier")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ColorHex = "#FF4B2B",
                            CreatedDate = new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(744),
                            Icon = "fitness",
                            IsDeleted = false,
                            Name = "Physical",
                            XPMultiplier = 1.2
                        },
                        new
                        {
                            Id = 2,
                            ColorHex = "#AF40FF",
                            CreatedDate = new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1513),
                            Icon = "school",
                            IsDeleted = false,
                            Name = "Learning",
                            XPMultiplier = 1.5
                        },
                        new
                        {
                            Id = 3,
                            ColorHex = "#2196F3",
                            CreatedDate = new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1515),
                            Icon = "work",
                            IsDeleted = false,
                            Name = "Work",
                            XPMultiplier = 1.0
                        },
                        new
                        {
                            Id = 4,
                            ColorHex = "#4CAF50",
                            CreatedDate = new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1516),
                            Icon = "groups",
                            IsDeleted = false,
                            Name = "Social",
                            XPMultiplier = 1.1000000000000001
                        },
                        new
                        {
                            Id = 5,
                            ColorHex = "#FFC107",
                            CreatedDate = new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1517),
                            Icon = "self-improvement",
                            IsDeleted = false,
                            Name = "Mental",
                            XPMultiplier = 1.3
                        },
                        new
                        {
                            Id = 6,
                            ColorHex = "#E91E63",
                            CreatedDate = new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1518),
                            Icon = "palette",
                            IsDeleted = false,
                            Name = "Creative",
                            XPMultiplier = 1.3999999999999999
                        });
                });

            modelBuilder.Entity("LifeOs.Entities.UserActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("integer");

                    b.Property<int>("EarnedXP")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("UserActivities");
                });

            modelBuilder.Entity("LifeOs.Entities.UserActivity", b =>
                {
                    b.HasOne("LifeOs.Entities.Category", "Category")
                        .WithMany("UserActivities")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LifeOs.Entities.Category", b =>
                {
                    b.Navigation("UserActivities");
                });
#pragma warning restore 612, 618
        }
    }
}
