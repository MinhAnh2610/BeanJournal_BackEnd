﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241022081512_RemoveMinhAnh")]
    partial class RemoveMinhAnh
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileImagePublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpirationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7c9f577f-8a87-4c15-9306-b51848c2ac3b",
                            CreatedAt = new DateTime(2024, 10, 22, 15, 15, 9, 856, DateTimeKind.Local).AddTicks(8291),
                            Email = "soybean@example.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "SOYBEAN@EXAMPLE.COM",
                            NormalizedUserName = "SOYBEAN",
                            PasswordHash = "AQAAAAIAAYagAAAAEOW0IfR8n6vCHKcNdY3HJvzBE5qQh3SGhdk2NYvPvV4PrVejmjHwjLHBtfTGf28PCw==",
                            PhoneNumberConfirmed = false,
                            ProfileImagePublicId = "",
                            ProfileImageUrl = "",
                            RefreshToken = "",
                            RefreshTokenExpirationDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "29a0e19c-6e5a-4d7b-b474-015d2461ef76",
                            TwoFactorEnabled = false,
                            UserName = "soybean"
                        },
                        new
                        {
                            Id = "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d81e6a9a-d634-4460-a5fb-c9d6605c0338",
                            CreatedAt = new DateTime(2024, 10, 22, 15, 15, 9, 856, DateTimeKind.Local).AddTicks(8688),
                            Email = "greenbean@example.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "GREENBEAN@EXAMPLE.COM",
                            NormalizedUserName = "GREENBEAN",
                            PasswordHash = "AQAAAAIAAYagAAAAEDuk/UAy3FFGicBryuMBGvLdlSjgQk8yjo3tXXJnjcXC+oO4L10z/xxllGuKuDJM7Q==",
                            PhoneNumberConfirmed = false,
                            ProfileImagePublicId = "",
                            ProfileImageUrl = "",
                            RefreshToken = "",
                            RefreshTokenExpirationDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "74b0d82f-2ef7-4c9b-92cb-8a4e94db1f3d",
                            TwoFactorEnabled = false,
                            UserName = "greenbean"
                        });
                });

            modelBuilder.Entity("Entities.DiaryEntry", b =>
                {
                    b.Property<int>("EntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EntryId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mood")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EntryId");

                    b.HasIndex("UserId");

                    b.ToTable("DiaryEntries");

                    b.HasData(
                        new
                        {
                            EntryId = 1,
                            Content = "Went to the park today, it was sunny and relaxing.",
                            CreatedAt = new DateTime(2024, 8, 10, 9, 15, 0, 0, DateTimeKind.Unspecified),
                            Mood = "Happy",
                            Title = "A Day at the Park",
                            UpdatedAt = new DateTime(2024, 8, 10, 9, 15, 0, 0, DateTimeKind.Unspecified),
                            UserId = "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC"
                        },
                        new
                        {
                            EntryId = 2,
                            Content = "Spent some time thinking about life, feeling a bit melancholy.",
                            CreatedAt = new DateTime(2024, 8, 11, 20, 30, 0, 0, DateTimeKind.Unspecified),
                            Mood = "Thinking",
                            Title = "Reflective Evening",
                            UpdatedAt = new DateTime(2024, 8, 11, 20, 30, 0, 0, DateTimeKind.Unspecified),
                            UserId = "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC"
                        },
                        new
                        {
                            EntryId = 3,
                            Content = "Had a great start today, finished a lot of tasks and feeling accomplished.",
                            CreatedAt = new DateTime(2024, 8, 12, 7, 45, 0, 0, DateTimeKind.Unspecified),
                            Mood = "Energetic",
                            Title = "Productive Morning",
                            UpdatedAt = new DateTime(2024, 8, 12, 7, 45, 0, 0, DateTimeKind.Unspecified),
                            UserId = "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC"
                        });
                });

            modelBuilder.Entity("Entities.EntryTag", b =>
                {
                    b.Property<int>("EntryId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("EntryId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("EntryTags");

                    b.HasData(
                        new
                        {
                            EntryId = 1,
                            TagId = 1
                        },
                        new
                        {
                            EntryId = 1,
                            TagId = 9
                        },
                        new
                        {
                            EntryId = 1,
                            TagId = 10
                        },
                        new
                        {
                            EntryId = 2,
                            TagId = 2
                        },
                        new
                        {
                            EntryId = 2,
                            TagId = 5
                        },
                        new
                        {
                            EntryId = 2,
                            TagId = 10
                        },
                        new
                        {
                            EntryId = 3,
                            TagId = 3
                        },
                        new
                        {
                            EntryId = 3,
                            TagId = 7
                        },
                        new
                        {
                            EntryId = 3,
                            TagId = 9
                        });
                });

            modelBuilder.Entity("Entities.MediaAttachment", b =>
                {
                    b.Property<int>("MediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MediaId"));

                    b.Property<long>("Bytes")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntryId")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("MediaId");

                    b.HasIndex("EntryId");

                    b.ToTable("MediaAttachments");

                    b.HasData(
                        new
                        {
                            MediaId = 1,
                            Bytes = 21079L,
                            CreatedAt = new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EntryId = 1,
                            FilePath = "https://res.cloudinary.com/dp34so8og/image/upload/v1728888961/r714qjypzjk74xzacfge.jpg",
                            FileType = "jpg",
                            Height = 500,
                            PublicId = "r714qjypzjk74xzacfge",
                            Width = 500
                        },
                        new
                        {
                            MediaId = 2,
                            Bytes = 25927L,
                            CreatedAt = new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EntryId = 1,
                            FilePath = "https://res.cloudinary.com/dp34so8og/image/upload/v1728888962/mhi7gxgc5npedgsrxywk.jpg",
                            FileType = "jpg",
                            Height = 500,
                            PublicId = "mhi7gxgc5npedgsrxywk",
                            Width = 500
                        },
                        new
                        {
                            MediaId = 3,
                            Bytes = 28629L,
                            CreatedAt = new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EntryId = 2,
                            FilePath = "https://res.cloudinary.com/dp34so8og/image/upload/v1728889003/vwck4ehodogpda1mm0yb.jpg",
                            FileType = "jpg",
                            Height = 500,
                            PublicId = "vwck4ehodogpda1mm0yb",
                            Width = 500
                        },
                        new
                        {
                            MediaId = 4,
                            Bytes = 30154L,
                            CreatedAt = new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EntryId = 2,
                            FilePath = "https://res.cloudinary.com/dp34so8og/image/upload/v1728889005/dhvolfscyn4tubn0pfmd.jpg",
                            FileType = "jpg",
                            Height = 500,
                            PublicId = "dhvolfscyn4tubn0pfmd",
                            Width = 500
                        });
                });

            modelBuilder.Entity("Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("IconPublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            IconPublicId = "jwd7ie3xvilsxdwfoer1",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728839713/jwd7ie3xvilsxdwfoer1.png",
                            ImagePublicId = "j0yfhumpqfkvtejv4qrq",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728839712/j0yfhumpqfkvtejv4qrq.png",
                            Name = "Nature"
                        },
                        new
                        {
                            TagId = 2,
                            IconPublicId = "e8wncdvvpkopwm8iqry0",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728839984/e8wncdvvpkopwm8iqry0.png",
                            ImagePublicId = "cnm2tbtnpir8nojamcyz",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728839983/cnm2tbtnpir8nojamcyz.jpg",
                            Name = "Reflection"
                        },
                        new
                        {
                            TagId = 3,
                            IconPublicId = "ri0uqdcwwctci8wkbskg",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840425/ri0uqdcwwctci8wkbskg.png",
                            ImagePublicId = "qxjv8bfeiqlxkuu33a0y",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840423/qxjv8bfeiqlxkuu33a0y.png",
                            Name = "Productivity"
                        },
                        new
                        {
                            TagId = 4,
                            IconPublicId = "i0soxznbecb5ghdaykht",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840306/i0soxznbecb5ghdaykht.png",
                            ImagePublicId = "b9ziqgts97rfo8hqtzwt",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840305/b9ziqgts97rfo8hqtzwt.png",
                            Name = "Travel"
                        },
                        new
                        {
                            TagId = 5,
                            IconPublicId = "qmngr2kphivtnl1t9bzr",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840496/qmngr2kphivtnl1t9bzr.png",
                            ImagePublicId = "ggonlsvw4usddsgiiklr",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840495/ggonlsvw4usddsgiiklr.jpg",
                            Name = "Wellness"
                        },
                        new
                        {
                            TagId = 6,
                            IconPublicId = "k0lhbljnerejuvrs2e6l",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840568/k0lhbljnerejuvrs2e6l.png",
                            ImagePublicId = "sfbxhk7ta5nbwyysfd9o",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840567/sfbxhk7ta5nbwyysfd9o.png",
                            Name = "Creativity"
                        },
                        new
                        {
                            TagId = 7,
                            IconPublicId = "sxci6phuvxbttoqz3uwh",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840611/sxci6phuvxbttoqz3uwh.png",
                            ImagePublicId = "h1uvqhtili9m9wpbtkji",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840610/h1uvqhtili9m9wpbtkji.png",
                            Name = "Work"
                        },
                        new
                        {
                            TagId = 8,
                            IconPublicId = "lv6ptrj3iipp6qhrn1oi",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840658/lv6ptrj3iipp6qhrn1oi.png",
                            ImagePublicId = "y7dvs5fuohoyrhqyfzhu",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840657/y7dvs5fuohoyrhqyfzhu.jpg",
                            Name = "Family"
                        },
                        new
                        {
                            TagId = 9,
                            IconPublicId = "lubu7shjfvcio6ry31qc",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840750/lubu7shjfvcio6ry31qc.png",
                            ImagePublicId = "mrqm4834ikuv6nazjkxj",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840749/mrqm4834ikuv6nazjkxj.jpg",
                            Name = "Mindfulness"
                        },
                        new
                        {
                            TagId = 10,
                            IconPublicId = "tozy6kua45sckbcwaa6k",
                            IconUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840963/tozy6kua45sckbcwaa6k.png",
                            ImagePublicId = "s9uyjgv32hkjh4etpl5x",
                            ImageUrl = "http://res.cloudinary.com/dp34so8og/image/upload/v1728840962/s9uyjgv32hkjh4etpl5x.jpg",
                            Name = "Self-improvement"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator().HasValue("IdentityRole");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                            RoleId = "F6F6F8BD-F92A-43EF-A8D9-CCC665D5021F"
                        },
                        new
                        {
                            UserId = "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                            RoleId = "0508330E-790A-497C-A84A-5DE5E0D8367B"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Entities.ApplicationRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.HasDiscriminator().HasValue("ApplicationRole");

                    b.HasData(
                        new
                        {
                            Id = "F6F6F8BD-F92A-43EF-A8D9-CCC665D5021F",
                            ConcurrencyStamp = "a3e9f59f-15dc-4b9f-bc4e-06a39e5a9c6a",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "0508330E-790A-497C-A84A-5DE5E0D8367B",
                            ConcurrencyStamp = "84cc659b-8d62-4299-8473-4c905a79bb0d",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Entities.DiaryEntry", b =>
                {
                    b.HasOne("Entities.ApplicationUser", "User")
                        .WithMany("DiaryEntries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.EntryTag", b =>
                {
                    b.HasOne("Entities.DiaryEntry", "Entry")
                        .WithMany("EntryTags")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Tag", "Tag")
                        .WithMany("EntryTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Entities.MediaAttachment", b =>
                {
                    b.HasOne("Entities.DiaryEntry", "Entry")
                        .WithMany("MediaAttachments")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.ApplicationUser", b =>
                {
                    b.Navigation("DiaryEntries");
                });

            modelBuilder.Entity("Entities.DiaryEntry", b =>
                {
                    b.Navigation("EntryTags");

                    b.Navigation("MediaAttachments");
                });

            modelBuilder.Entity("Entities.Tag", b =>
                {
                    b.Navigation("EntryTags");
                });
#pragma warning restore 612, 618
        }
    }
}
